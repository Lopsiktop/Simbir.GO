using ErrorOr;

namespace Simbir.GO.Application.Common.Interfaces.Authentication;

public interface ICheckAccounts
{
    Task<ErrorOr<bool>> TokenIsRevoked(string token);

    Task<bool> AccountDoesExist(int userId);
}
