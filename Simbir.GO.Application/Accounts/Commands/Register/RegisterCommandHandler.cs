using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.AccountEntity;

namespace Simbir.GO.Application.Accounts.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AccountResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AccountResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var account = Account.Create(request.Username, request.Password);

        var result = await _unitOfWork.AccountRepository.Add(account);

        if (result.HasValue)
            return result.Value;

        await _unitOfWork.SaveChangesAsync();

        return new AccountResult(account.Id, account.Username, account.Balance);
    }
}
