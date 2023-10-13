using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminAccounts.Commands.DeleteAccount;

internal class DeleteAccountAdminCommandHandler : IRequestHandler<DeleteAccountAdminCommand, Error?>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Error?> Handle(DeleteAccountAdminCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId < 0)
            return Errors.General.UserIdCannotBeNegative;

        var result = await _unitOfWork.AccountRepository.Remove(request.UserId);

        if (result.HasValue)
            return result.Value;

        await _unitOfWork.SaveChangesAsync();

        return null;
    }
}
