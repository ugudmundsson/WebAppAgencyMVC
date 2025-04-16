using Busniess.Interfaces;
using Busniess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAssignment.Models;

namespace WebAppAssignment.Controllers
{
    public class TeamMembersController(IProjectService projectService, IStatusService statusService, IAppUserService appUserService) : Controller
    {

        private readonly IProjectService _projectService = projectService;
        private readonly IStatusService _statusService = statusService;
        private readonly IAppUserService _appUserService = appUserService;

        [Authorize(Roles = "User")]
        public async Task<IActionResult> TeamMembers()
        {

            var membersResult = await _appUserService.GetAppUsersAsync();


            var member = membersResult.Result?.ToList() ?? new List<AppUser>();

            var projectViewModel = new ProjectViewModel
            {
                
                TeamMembers = member,
                Form = new AddProjectModel(),
                
            };
            return View(projectViewModel);
        }
    }
}
