﻿using ErrorOr;
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

    public async Task<Account?> FindById(int Id)
    {
        return await _context.Accounts.FindAsync(Id);
    }

    public Task<Account?> GetAccountByUsername(string username)
    {
        return _context.Accounts.SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<List<Account>> GetAllAccount(int start, int count)
    {
        return await _context.Accounts.Skip(start).Take(count).ToListAsync();
    }

    public async Task<Error?> Remove(int UserId)
    {
        var account = await FindById(UserId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        _context.Accounts.Remove(account);

        return null;
    }
}
