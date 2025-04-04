using Busniess.Interfaces;
using Busniess.Models;
using Data.Interfaces;
using Domain.Extensions;

namespace Busniess.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{

    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ClientResult> GetClientsAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }

}
