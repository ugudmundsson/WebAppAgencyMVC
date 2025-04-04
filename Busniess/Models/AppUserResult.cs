using Domain.Models;

namespace Busniess.Models;

public class AppUserResult : ServiceResult
{
    public IEnumerable<AppUser>? Result { get; set; }
}
