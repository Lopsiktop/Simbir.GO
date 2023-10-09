using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Infrastructure.Persistence.Repositories;

namespace Simbir.GO.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly SimbirDbContext _context;

    public UnitOfWork(SimbirDbContext context)
    {
        _context = context;
        AccountRepository = new AccountRepository(context);
    }

    public IAccountRepository AccountRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
