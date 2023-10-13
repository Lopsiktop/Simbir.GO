using ErrorOr;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Infrastructure.Authentication;

internal class CheckAccounts : ICheckAccounts
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckAccounts(IUnitOfWork unitOfWork)
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

    public async Task<bool> AccountDoesExist(int userId)
    {
        var account = await _unitOfWork.AccountRepository.FindById(userId);
        if (account is null)
            return false;

        return true;
    }
}
