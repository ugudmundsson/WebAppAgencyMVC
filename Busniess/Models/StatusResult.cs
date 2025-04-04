using Domain.Models;

namespace Busniess.Models;

public class StatusResult<T> : ServiceResult
{
    public T? Result { get; set; }
}

public class StatusResult : ServiceResult
{
}
