using ErrorOr;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.Common.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Error?> Add(Account account);

    Task<Account?> GetAccountByUsername(string username);

    Task<Account?> FindById(int Id);

    Task<List<Account>> GetAllAccount(int start, int count);

    Task<Error?> Remove(int UserId);
}
