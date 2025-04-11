using Busniess.Interfaces;
using Busniess.Models;
using Data.Entites;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;

namespace Busniess.Services;

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;


    public async Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData)
    {
        if (formData == null)
        {
            return new ProjectResult { Success = false, StatusCode = 400, Error = "Project data is null." };
        }

        var projectEntity = formData.MapTo<ProjectEntity>();
        var statusResult = await _statusService.GetStatusByIdAsync("1");
        var status = statusResult.Result;

        projectEntity.StatusId = status!.Id;


        projectEntity.ProjectTeamMember = formData.AppUserId.Select(userId => new ProjectTeamMemberEntity
        {
            AppUserId = userId,
            ProjectId = projectEntity.Id
        }).ToList();



        var result = await _projectRepository.AddAsync(projectEntity);
        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 201 }
            : new ProjectResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };

    }



    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync
        (
            orderByDescending: true,
            sortBy: s => s.CreatedAt,
            where: null,
            i => i.ProjectTeamMember,
            i => i.Status
            
        );
        return new ProjectResult<IEnumerable<Project>> { Success = true, StatusCode = 200, Result = response.Result };
    }




    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
        (
            where: i => i.Id == id,
            i => i.ProjectTeamMember,
            i => i.Status
            
        );
        return response.Success
            ? new ProjectResult<Project> { Success = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Success = false, StatusCode = 404, Error = $"Project '{id}' wasn't found." };
    }



}
