using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.AdminAccounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminAccounts.Commands.UpdateAccount;

internal class UpdateAccountAdminCommandHandler : IRequestHandler<UpdateAccountAdminCommand, ErrorOr<AccountAdminResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountAdminResult>> Handle(UpdateAccountAdminCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.FindById(request.UserId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        var user_with_this_username_does_exist = await _unitOfWork.AccountRepository.GetAccountByUsername(request.Username);

        if (user_with_this_username_does_exist is not null)
            return Errors.Account.UsernameMustBeUnique;

        var errors = account.Update(request.Username, request.Password, request.IsAdmin, request.Balance);

        if (errors.Count != 0)
            return errors;

        await _unitOfWork.SaveChangesAsync();

        return new AccountAdminResult(account.Id, account.Username, account.Balance, account.IsAdmin);
    }
}
