using Simbir.GO.Application.Common.Interfaces.Repositories;

namespace Simbir.GO.Application.Common.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }

    IRevokedTokenRepository RevokedTokenRepository { get; }

    ITransportRepository TransportRepository { get; }

    Task SaveChangesAsync();
}
