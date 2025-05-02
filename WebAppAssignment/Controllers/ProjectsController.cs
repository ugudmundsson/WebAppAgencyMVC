using Busniess.Interfaces;
using Busniess.Models;
using Data.Data;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WebAppAssignment.Models;

namespace WebAppAssignment.Controllers;

public class ProjectsController(IProjectService projectService, IStatusService statusService, IAppUserService appUserService) : Controller
{

    private readonly IProjectService _projectService = projectService;
    private readonly IStatusService _statusService = statusService;
    private readonly IAppUserService _appUserService = appUserService;

    [Authorize(Roles = "User")]
    public async Task<IActionResult> Projects()
    {
        var projectsResult = await _projectService.GetProjectsAsync();
        var statusesResult = await _statusService.GetStatusesAsync();
        var membersResult = await _appUserService.GetAppUsersAsync();

        var project = projectsResult.Result?.ToList() ?? new List<Project>();
        var status = statusesResult.Result?.ToList() ?? new List<Status>();
        var member = membersResult.Result?.ToList() ?? new List<AppUser>();

        var projectViewModel = new ProjectViewModel
        {
            Statuses = status,
            TeamMembers = member,
            Form = new AddProjectModel(),
            Projects = project,
        };

        var projects = projectsResult.Result?.ToList() ?? new List<Project>();

        var nextEnding = projects
            .Where(a => a.EndDate > DateTime.Now)
            .OrderBy(a => a.EndDate)
            .FirstOrDefault();
        if (nextEnding != null)
        {
            ViewBag.DaysLeft = (nextEnding.EndDate - DateTime.Now).Days;
        }
        else
        {
            ViewBag.DaysLeft = 0;
        }

            return View(projectViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Projects(ProjectViewModel model)
    {

        var AddProjectformData = model.Form.MapTo<AddProjectFormData>();

        var result = await _projectService.CreateProjectAsync(AddProjectformData);

        return RedirectToAction("Projects");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteProject(string id)
    {

        var result = await _projectService.RemoveAsync(id);
        if (result.Success)
        {
            return RedirectToAction("Projects");
        }

        return RedirectToAction("Projects");
    }


    [HttpPost]
    public async Task<IActionResult> UpdateProject(ProjectViewModel model)
    {
        var project = model.Form.MapTo<EditProjectFormData>();
        var result = await _projectService.UpdateAsync(project);
        if (result.Success)
        {
            return RedirectToAction("Projects");
        }
        return RedirectToAction("Projects");
    }





    [HttpGet]
    public async Task<IActionResult> UpdateProject(string id)
    {
        var projectsResult = await _projectService.GetProjectAsync(id);
        var statusesResult = await _statusService.GetStatusesAsync();
        var membersResult = await _appUserService.GetAppUsersAsync();

        
        var status = statusesResult.Result?.ToList() ?? new List<Status>();
        var member = membersResult.Result?.ToList() ?? new List<AppUser>();

        var projectViewModel = new ProjectViewModel
        {
            Form = projectsResult.Result!.MapTo<AddProjectModel>(),
            Projects = new List<Project>(),
            Statuses = status,
            TeamMembers = member,
        };
        return PartialView("Partials/_EditProjectPartial", projectViewModel);
    }


}
