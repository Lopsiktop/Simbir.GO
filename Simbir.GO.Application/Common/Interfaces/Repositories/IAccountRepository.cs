using ErrorOr;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.Common.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Error?> Add(Account account);
}
