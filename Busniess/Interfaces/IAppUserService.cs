using Busniess.Models;
using Domain.Models;

namespace Busniess.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUserResult> AddUserToRole(string userID, string roleName);
        Task<AppUserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User");
        Task<AppUserResult> GetAppUsersAsync();
    }
}