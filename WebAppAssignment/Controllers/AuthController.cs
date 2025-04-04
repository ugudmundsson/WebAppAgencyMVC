using Busniess.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppAssignment.Models;
using Domain.Models;
using Busniess.Interfaces;
using Domain.Extensions;

namespace WebAppAssignment.Controllers;

public class AuthController(SignInManager<AppUserEntity> signInManager, IAuthService authService) : Controller
{
    
    private readonly SignInManager<AppUserEntity> _signInManager = signInManager;
    private readonly IAuthService _authService = authService;



    // ----------------- SIGNUP -----------------

    public IActionResult SignUp()
    {
           return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignUp(RegistrationModel model)
    {
        ViewBag.Error = null;

        if (!ModelState.IsValid)
            return View(model);

        var signUpformData = model.MapTo<SignUpFormData>();

        var result = await _authService.SignUpAsync(signUpformData);
        if (result.Success)
        {
            return RedirectToAction("SignIn", "Auth");
        }


        ViewBag.Error = result.Error;
        return View();
    }




    // ----------------- SIGNIN -----------------

    public IActionResult SignIn(string returnURL = "~/")
    {
        ViewBag.ReturnUrl = returnURL;

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> SignIn(LoginModel model, string returnURL = "~/")
    {
        ViewBag.Error = null;
        ViewBag.ReturnUrl = returnURL;

        if (!ModelState.IsValid)
            return View(model);

        var signInformData = model.MapTo<SignInFormData>();

        var result = await _authService.SignInAsync(signInformData);
        if (result.Success)
        {
            return LocalRedirect(returnURL);
        }


        ViewBag.Error = result.Error;
        return View();
    }

    //---------------- SIGNOUT -----------------

    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Hero", "Projects");
    }


}
