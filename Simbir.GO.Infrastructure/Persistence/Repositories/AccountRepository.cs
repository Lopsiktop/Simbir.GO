using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Infrastructure.Persistence.Repositories;

internal class AccountRepository : IAccountRepository
{
    private readonly SimbirDbContext _context;

    public AccountRepository(SimbirDbContext context)
    {
        _context = context;
    }

    public async Task<Error?> Add(Account account)
    {
        var check_username = await _context.Accounts.SingleOrDefaultAsync(x => x.Username == account.Username);

        if (check_username is not null)
            return Errors.Account.UsernameMustBeUnique;

        await _context.Accounts.AddAsync(account);

        return null;
    }
}
