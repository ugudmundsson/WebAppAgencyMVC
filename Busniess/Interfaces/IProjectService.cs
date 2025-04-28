using Busniess.Models;
using Domain.Models;

namespace Busniess.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectResult> CreateProjectAsync(AddProjectFormData formData);
        Task<ProjectResult<Project>> GetProjectAsync(string id);
        Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
        Task<ProjectResult<IEnumerable<Project>>> GetProjectsByStatusIdAsync(string statusId);
        Task<ProjectResult> RemoveAsync(string id);
        Task<ProjectResult> UpdateAsync(EditProjectFormData formData);
    }
}