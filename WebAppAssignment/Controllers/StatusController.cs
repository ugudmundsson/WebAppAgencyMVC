using Microsoft.AspNetCore.Mvc;

namespace WebAppAssignment.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Started()
        {
            return View();
        }

        public IActionResult Completed()
        {
            return View();
        }
    }
}
