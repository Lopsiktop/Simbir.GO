using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Infrastructure.Persistence.Repositories;

internal class RevokedTokenRepository : IRevokedTokenRepository
{
    private readonly SimbirDbContext _context;

    public RevokedTokenRepository(SimbirDbContext context)
    {
        _context = context;
    }

    public async Task Add(RevokedToken token)
    {
        await _context.RevokedTokens.AddAsync(token);
    }

    public async Task<bool> TokenDoesExist(RevokedToken token)
    {
        var result = await _context.RevokedTokens.SingleOrDefaultAsync(x => x.Token == token.Token);

        if (result is not null)
            return true;

        return false;
    }
}
