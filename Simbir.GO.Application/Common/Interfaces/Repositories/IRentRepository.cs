using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Application.Common.Interfaces.Repositories;

public interface IRentRepository
{
    Task<List<Transport>> GetTransportsByLatAndLong(double latitude, double longitude, double radius, TransportType? type);
}
