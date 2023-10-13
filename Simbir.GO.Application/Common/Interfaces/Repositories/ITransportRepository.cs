using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Application.Common.Interfaces.Repositories;

public interface ITransportRepository
{
    Task<Transport?> FindById(int transportId);

    Task Add(Transport transport);
}