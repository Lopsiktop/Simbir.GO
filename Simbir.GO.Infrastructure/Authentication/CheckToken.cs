using ErrorOr;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Infrastructure.Authentication;

internal class CheckToken : ICheckToken
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckToken(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> TokenIsRevoked(string token)
    {
        var tokenRevokedResult = RevokedToken.Create(token, default);

        if (tokenRevokedResult.IsError)
            return tokenRevokedResult.FirstError;

        var tokenRevoked = tokenRevokedResult.Value;

        return await _unitOfWork.RevokedTokenRepository.TokenDoesExist(tokenRevoked);
    }
}
