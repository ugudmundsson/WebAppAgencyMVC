using Busniess.Models;
using Domain.Models;

namespace Busniess.Interfaces
{
    public interface IStatusService
    {
        Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync();
        Task<StatusResult<Status>> GetStatusByNameAsync(string statusName);
        Task<StatusResult<Status>> GetStatusByIdAsync(string id);
    }
}