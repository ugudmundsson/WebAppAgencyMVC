using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppAssignment.Models;

namespace WebAppAssignment.Controllers
{
    public class ProjectsController : Controller
    {



        public IActionResult Projects()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Projects(AddProjectModel model)
        {
            ViewBag.Error = null;

            if (!ModelState.IsValid)
                return View(model);

            var AddProjectformData = model.MapTo<AddProjectFormData>();

            var result = await _authService.CreateProjectAsync(AddProjectformData);
            if (result.Success)
            {
                return RedirectToAction("SignIn", "Auth");
            }


            ViewBag.Error = result.Error;
            return View();
        }



    }
}
