using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Accounts.Queries.GetMe;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, ErrorOr<AccountResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountResult>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.FindById(request.UserId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        return new AccountResult(account.Id, account.Username, account.Balance);
    }
}
