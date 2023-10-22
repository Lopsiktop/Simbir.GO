namespace Simbir.GO.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, bool isAdmin);
}
