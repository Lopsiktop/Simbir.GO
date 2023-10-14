using ErrorOr;
using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Infrastructure.Persistence.Repositories;

internal class TransportRepository : ITransportRepository
{
    private readonly SimbirDbContext _context;

    public TransportRepository(SimbirDbContext context)
    {
        _context = context;
    }

    public async Task Add(Transport transport)
    {
        await _context.Transports.AddAsync(transport);
    }

    public async Task<Transport?> FindById(int transportId)
    {
        return await _context.Transports.FindAsync(transportId);
    }

    public void Remove(Transport transport)
    {
        _context.Transports.Remove(transport);
    }
}
