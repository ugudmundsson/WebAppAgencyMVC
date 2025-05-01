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




    // ------------------ CREATE -----------------------------
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


        projectEntity.ProjectTeamMember = formData.Members.Select(userId => new ProjectTeamMemberEntity
        {
            AppUserId = userId,
            ProjectId = projectEntity.Id
        }).ToList();



        var result = await _projectRepository.AddAsync(projectEntity);
        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 201 }
            : new ProjectResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };

    }



    // -------------------- READ ----------------------

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

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsByStatusIdAsync(string statusId)
    {
        var response = await _projectRepository.GetAllAsync
        (
            orderByDescending: true,
            sortBy: s => s.CreatedAt,
            where: i => i.StatusId == statusId,
            i => i.ProjectTeamMember,
            i => i.Status
        );
        return new ProjectResult<IEnumerable<Project>> { Success = true, StatusCode = 200, Result = response.Result };
    }



    // --------------------- UPDATE -----------------


    public async Task<ProjectResult> UpdateAsync(EditProjectFormData formData)
    {
      
            var projectEntity = new ProjectEntity{

                Id = formData.Id,
                Image = formData.Image,
                ProjectName = formData.ProjectName,
                ClientName = formData.ClientName,
                Description = formData.Description,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                StatusId = formData.StatusId,
                Budget = formData.Budget,
                ProjectTeamMember = formData.Members.Select(userId => new ProjectTeamMemberEntity
                {
                    AppUserId = userId,
                    ProjectId = formData.Id,
                }).ToList()
            };
        var result = await _projectRepository.UpdateWithAll(projectEntity);
        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 200 }
            : new ProjectResult { Success = false, StatusCode = 500, Error = result.Error };
    }

 








    // --------------------- DELETE ------------------
    public async Task<ProjectResult> RemoveAsync(string id)
    {
        var result = await _projectRepository.RemoveAsync(x => x.Id == id);

        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 200 }
            : new ProjectResult { Success = false, StatusCode = 500, Error = result.Error };
    }

}
