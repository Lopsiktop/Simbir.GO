using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminAccounts.Queries;

internal class GetAllAccountsAdminQueryHandler : IRequestHandler<GetAllAccountsAdminQuery, ErrorOr<List<AccountAdminResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAccountsAdminQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<AccountAdminResult>>> Handle(GetAllAccountsAdminQuery request, CancellationToken cancellationToken)
    {
        if (request.Start < 0)
            return Errors.General.StartNumberCannotBeNegative;

        if (request.Count <= 0)
            return Errors.General.CountNumberCannotBeLessThanZero;

        var accounts = await _unitOfWork.AccountRepository.GetAllAccount(request.Start, request.Count);
        var result = accounts.Select(x => new AccountAdminResult(x.Id, x.Username, x.Balance, x.IsAdmin)).ToList();
        return result;
    }
}
