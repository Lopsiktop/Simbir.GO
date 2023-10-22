using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Payment.Commands.AddMoney;

internal class AddMoneyCommandHandler : IRequestHandler<AddMoneyCommand, ErrorOr<AccountResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddMoneyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountResult>> Handle(AddMoneyCommand request, CancellationToken cancellationToken)
    {
        var fromUser = await _unitOfWork.AccountRepository.FindById(request.FromUserId);
        if (fromUser is null)
            return Errors.Account.AccountDoesNotExist;

        var toUser = await _unitOfWork.AccountRepository.FindById(request.ToUserId);
        if (toUser is null)
            return Errors.Account.AccountDoesNotExist;

        bool can = false;

        if (fromUser.Id == toUser.Id || fromUser.IsAdmin)
            can = true;

        if (!can)
            return Errors.General.OnlyForAdmin;

        toUser.AddMoney();
        await _unitOfWork.SaveChangesAsync();

        return new AccountResult(toUser.Id, toUser.Username, toUser.Balance);
    }
}
