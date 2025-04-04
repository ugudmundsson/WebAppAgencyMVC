using Microsoft.AspNetCore.Mvc;

namespace WebAppAssignment.Controllers
{
    public class HeroController : Controller
    {
        public IActionResult Hero()
        {
            return View();
        }
    }
}
