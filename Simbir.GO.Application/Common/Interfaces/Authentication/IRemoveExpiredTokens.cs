namespace Simbir.GO.Application.Common.Interfaces.Authentication;

public interface IRemoveExpiredTokens
{
    Task RemoveExpiredJwtTokens();
}
