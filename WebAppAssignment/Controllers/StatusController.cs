using Busniess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAppAssignment.Models;


namespace WebAppAssignment.Controllers
{
    public class StatusController : Controller
    {
        private readonly IProjectService _projectService;

        public StatusController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> All()
        {
            var projectResult = (await _projectService.GetProjectsAsync()).Result;

            return PartialView("Partials/_ProjectListPartial", projectResult);
        }

        public async Task<IActionResult> Started()
        {
            var projectsResult = (await _projectService.GetProjectsByStatusIdAsync("1")).Result;

            return PartialView("Partials/_ProjectListPartial", projectsResult);
        }

        public async Task<IActionResult> Completed()
        {
            var projectsResult = (await _projectService.GetProjectsByStatusIdAsync("2")).Result;
            return PartialView("Partials/_ProjectListPartial", projectsResult);
        }
    }
}
