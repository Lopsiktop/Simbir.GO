using ErrorOr;

namespace Simbir.GO.Application.Common.Interfaces.Authentication;

public interface ICheckToken
{
    Task<ErrorOr<bool>> TokenIsRevoked(string token);
}
