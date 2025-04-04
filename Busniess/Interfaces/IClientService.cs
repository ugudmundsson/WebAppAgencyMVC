using Busniess.Models;

namespace Busniess.Interfaces
{
    public interface IClientService
    {
        Task<ClientResult> GetClientsAsync();
    }
}