using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.AdminAccounts.Commands.CreateAccount;

internal class CreateAccountAdminCommandHandler : IRequestHandler<CreateAccountAdminCommand, ErrorOr<AccountAdminResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountAdminResult>> Handle(CreateAccountAdminCommand request, CancellationToken cancellationToken)
    {
        var accountResult = Account.Create(request.Username, request.Password, request.IsAdmin, request.Balance);

        if (accountResult.IsError)
            return accountResult.Errors;

        var account = accountResult.Value;

        var result = await _unitOfWork.AccountRepository.Add(account);

        if (result.HasValue)
            return result.Value;

        await _unitOfWork.SaveChangesAsync();

        return new AccountAdminResult(account.Id, account.Username, account.Balance, account.IsAdmin);
    }
}
