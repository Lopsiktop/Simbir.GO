using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminAccounts.Queries.GetAccount;

internal class GetAccountAdminQueryHandler : IRequestHandler<GetAccountAdminQuery, ErrorOr<AccountAdminResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountAdminQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountAdminResult>> Handle(GetAccountAdminQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId < 0)
            return Errors.General.UserIdCannotBeNegative;

        var account = await _unitOfWork.AccountRepository.FindById(request.UserId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        return new AccountAdminResult(account.Id, account.Username, account.Balance, account.IsAdmin);
    }
}
