using Busniess.Interfaces;
using Busniess.Models;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Busniess.Services;

public class AuthService(IAppUserService userService, SignInManager<AppUserEntity> signInManager) : IAuthService
{
    private readonly IAppUserService _userService = userService;
    private readonly SignInManager<AppUserEntity> _signInManager = signInManager;

    public async Task<AuthResult> SignInAsync(SignInFormData formData)
    {

        if (formData == null)
            return new AuthResult { Success = false, StatusCode = 400, Error = "User data is null." };

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersisent, false);
        return result.Succeeded
            ? new AuthResult { Success = true, StatusCode = 200 }
            : new AuthResult { Success = false, StatusCode = 401, Error = "Invalid password or email." };

    }

    public async Task<AuthResult> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
            return new AuthResult { Success = false, StatusCode = 400, Error = "User data is null." };

        var result = await _userService.CreateUserAsync(formData);
        return result.Success
            ? new AuthResult { Success = true, StatusCode = 201 }
            : new AuthResult { Success = false, StatusCode = result.StatusCode, Error = result.Error };

    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Success = true, StatusCode = 200 };
    }


}
