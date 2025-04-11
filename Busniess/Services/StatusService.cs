using Busniess.Interfaces;
using Busniess.Models;
using Data.Interfaces;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Busniess.Services;

public class StatusService(IStatusRepository statusRepository) : IStatusService
{

    private readonly IStatusRepository _statusRepository = statusRepository;

    public async Task<StatusResult<IEnumerable<Status>>> GetStatusesAsync()
    {
        var result = await _statusRepository.GetAllAsync();
        return result.Success
            ? new StatusResult<IEnumerable<Status>> { Success = true, StatusCode = 200, Result = result.Result }
            : new StatusResult<IEnumerable<Status>> { Success = false, StatusCode = result.StatusCode, Error = result.Error };
        
    }


    public async Task<StatusResult<Status>> GetStatusByNameAsync(string statusName)
    {
        var result = await _statusRepository.GetAsync(x => x.StatusName == statusName);
        return result.Success
             ? new StatusResult<Status> { Success = true, StatusCode = 200, Result = result.Result }
             : new StatusResult<Status> { Success = false, StatusCode = result.StatusCode, Error = result.Error };
    }


    public async Task<StatusResult<Status>> GetStatusByIdAsync(string id)
    {
        var result = await _statusRepository.GetAsync(x => x.Id == id);
        return result.Success
                ? new StatusResult<Status> { Success = true, StatusCode = 200, Result = result.Result }
                : new StatusResult<Status> { Success = false, StatusCode = result.StatusCode, Error = result.Error };
    }


}
