﻿using Busniess.Interfaces;
using Busniess.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppAssignment.Models;

namespace WebAppAssignment.Controllers
{
    public class TeamMembersController(IProjectService projectService, IStatusService statusService, IAppUserService appUserService) : Controller
    {

        private readonly IProjectService _projectService = projectService;
        private readonly IStatusService _statusService = statusService;
        private readonly IAppUserService _appUserService = appUserService;


        public async Task<IActionResult> TeamMembers()
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
    }
}
