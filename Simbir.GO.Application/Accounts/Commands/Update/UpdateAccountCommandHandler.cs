using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Accounts.Commands.Update;

internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ErrorOr<AccountResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountResult>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.FindById(request.UserId);

        if (account is null)
            return Errors.Account.AccountDoesNotExist;

        var user_with_this_username_does_exist = await _unitOfWork.AccountRepository.GetAccountByUsername(request.Username);

        if (user_with_this_username_does_exist is not null)
            return Errors.Account.UsernameMustBeUnique;

        var errors = account.Update(request.Username, request.Password);

        if (errors.Count != 0)
            return errors;

        await _unitOfWork.SaveChangesAsync();

        return new AccountResult(account.Id, account.Username, account.Balance);
    }
}
