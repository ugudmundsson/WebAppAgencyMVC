using Busniess.Interfaces;
using Busniess.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Busniess.Services;

public class AppUserService(IAppUserRepository appuserRepository, UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager) : IAppUserService
{

    private readonly IAppUserRepository _appuserRepository = appuserRepository;
    private readonly UserManager<AppUserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;



    // -------------------- CREATE ----------------------

    public async Task<AppUserResult> AddUserToRole(string userID, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new AppUserResult { Success = false, StatusCode = 404, Error = "Role not found" };

        var user = await _userManager.FindByIdAsync(userID);
        if (user == null)
            return new AppUserResult { Success = false, StatusCode = 200, Error = "User not found" };

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded
            ? new AppUserResult { Success = true, StatusCode = 200 }
            : new AppUserResult { Success = false, StatusCode = 500, Error = "Failed to add user to role" };
    }

    public async Task<AppUserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User")
    {
        if (formData == null)
            return new AppUserResult { Success = false, StatusCode = 400, Error = "User data is null." };

        var existsResult = await _appuserRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Success)
            return new AppUserResult { Success = false, StatusCode = 409, Error = "User already exists." };

        try
        {
            var userEntity = formData.MapTo<AppUserEntity>();

            userEntity.UserName = formData.Email;


            var result = await _userManager.CreateAsync(userEntity, formData.Password);
            if (result.Succeeded)
            {
                var addToRoleResult = await AddUserToRole(userEntity.Id, roleName);
                return result.Succeeded
                    ? new AppUserResult { Success = true, StatusCode = 201 }
                    : new AppUserResult { Success = false, StatusCode = 201, Error = "User created but not added to role." };

            }

            return new AppUserResult { Success = false, StatusCode = 500, Error = "Failed to create user" };


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new AppUserResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }


    // -------------------- READ ----------------------
    public async Task<AppUserResult> GetAppUsersAsync()
    {
        var result = await _appuserRepository.GetAllAsync();
        return result.MapTo<AppUserResult>();
    }



    // -------------------- UPDATE-----------------------








    // -------------------- DELETE ----------------------
    public async Task<ProjectResult> RemoveUserAsync(string id)
    {
        var result = await _appuserRepository.RemoveAsync(x => x.Id == id);

        return result.Success
            ? new ProjectResult { Success = true, StatusCode = 200 }
            : new ProjectResult { Success = false, StatusCode = 500, Error = result.Error };
    }


}