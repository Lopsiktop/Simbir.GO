using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Accounts.Queries.SingIn;

internal class SignInAccountQueryHandler : IRequestHandler<SignInAccountQuery, ErrorOr<TokenResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignInAccountQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<TokenResult>> Handle(SignInAccountQuery request, CancellationToken cancellationToken)
    {
        var result = Account.ValidateAccount(request.Username, request.Password);

        if (result.Count != 0)
            return result;

        var account = await _unitOfWork.AccountRepository.GetAccountByUsername(request.Username);

        if (account is null) 
            return Errors.Account.AccountDoesNotExist;

        bool isSucceded = account.VerifyPassword(request.Password);

        if(!isSucceded)
            return Errors.Account.AccountDoesNotExist;

        var token = _jwtTokenGenerator.GenerateToken(account.Id, account.IsAdmin);

        return new TokenResult(account.Id, account.Username, token);
    }
}
