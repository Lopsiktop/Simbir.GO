using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Infrastructure.Persistence.Repositories;

internal class RentRepository : IRentRepository
{
    private readonly SimbirDbContext _context;

    public RentRepository(SimbirDbContext context)
    {
        _context = context;
    }

    public Task<List<Transport>> GetTransportsByLatAndLong(double latitude, double longitude, double radius, TransportType? type)
    {
        //x^2 + y^2 <= R^2
        var allTransports = _context.Transports.Where(x => (Math.Pow(x.Longitude - longitude, 2) + Math.Pow(x.Latitude - latitude, 2)) <= Math.Pow(radius, 2));
        if (type is null)
            return allTransports.ToListAsync();

        return allTransports.Where(x => x.TransportType == type).ToListAsync();
    }
}
