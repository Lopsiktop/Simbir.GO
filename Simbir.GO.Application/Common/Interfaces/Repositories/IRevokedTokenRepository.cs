using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.Common.Interfaces.Repositories;

public interface IRevokedTokenRepository
{
    Task Add(RevokedToken token);

    Task<bool> TokenDoesExist(RevokedToken token); 
}