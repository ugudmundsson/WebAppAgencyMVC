using Microsoft.AspNetCore.Mvc;

namespace WebAppAssignment.Controllers
{
    public class TeamMembersController : Controller
    {
        public IActionResult TeamMembers()
        {
            return View();
        }
    }
}
