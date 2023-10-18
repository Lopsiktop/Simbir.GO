using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Simbir.GO.Application.Common.Interfaces.Repositories;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Infrastructure.Persistence.Repositories;

internal class RentRepository : IRentRepository
{
    private readonly SimbirDbContext _context;

    public RentRepository(SimbirDbContext context)
    {
        _context = context;
    }

    public async Task<Error?> Add(Rent rent)
    {
        var exist = await _context.Rents.SingleOrDefaultAsync(x => x.TransportId == rent.TransportId);
        if(exist is not null)
            return Errors.Rent.ThisTransportHasAlreadyRented;

        _context.Rents.Add(rent);
        return null;
    }

    public async Task<Rent?> FindById(int id)
    {
        return await _context.Rents.FindAsync(id);
    }

    public async Task<Rent?> FindByIdInclude(int id)
    {
        return await _context.Rents.Include(x => x.Transport).Include(x => x.Renter).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Rent>> GetRentsByUserId(int userId)
    {
        return _context.Rents.Where(x => x.RenterId == userId).ToListAsync();
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
