using Busniess.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebAppAssignment.Models;

namespace WebAppAssignment.Controllers
{
    public class ProjectsController(IProjectService projectService, IStatusService statusService, IAppUserService appUserService) : Controller
    {

        private readonly IProjectService _projectService = projectService;
        private readonly IStatusService _statusService = statusService;
        private readonly IAppUserService _appUserService = appUserService;



        
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
                Members = member,
                Form = new AddProjectModel(),
                Projects = project,
            };
            return View(projectViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Projects(ProjectViewModel model)
        {
         
            var AddProjectformData = model.Form.MapTo<AddProjectFormData>();

            var result = await _projectService.CreateProjectAsync(AddProjectformData);
   
            return RedirectToAction ("Projects");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(string id)
        {

                var result = await _projectService.RemoveAsync(id);
                if (result.Success)
                {
                    return RedirectToAction("Projects");
                }
            
            return RedirectToAction("Projects");
        }
    }
}
