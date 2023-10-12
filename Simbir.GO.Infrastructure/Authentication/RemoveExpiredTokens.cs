using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Infrastructure.Persistence;

namespace Simbir.GO.Infrastructure.Authentication;

internal class RemoveExpiredTokens : IRemoveExpiredTokens
{
    private readonly SimbirDbContext _context;

    public RemoveExpiredTokens(SimbirDbContext context)
    {
        _context = context;
    }

    public async Task RemoveExpiredJwtTokens()
    {
        var tokens = await _context.RevokedTokens.Where(x => x.ExpirationTimeUtc <= DateTime.UtcNow).ToArrayAsync();
        _context.RevokedTokens.RemoveRange(tokens);
    }
}
