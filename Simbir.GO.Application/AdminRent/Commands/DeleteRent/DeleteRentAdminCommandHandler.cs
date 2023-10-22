using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.AdminRent.Commands.DeleteRent;

internal class DeleteRentAdminCommandHandler : IRequestHandler<DeleteRentAdminCommand, Error?>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRentAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Error?> Handle(DeleteRentAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.AccountRepository.FindById(request.UserId);
        if (user is null)
            return Errors.Account.AccountDoesNotExist;

        if (!user.IsAdmin)
            return Errors.General.OnlyForAdmin;

        var rent = await _unitOfWork.RentRepository.FindById(request.RentId);
        if (rent is null)
            return Errors.Rent.RentDoesNotExist;

        _unitOfWork.RentRepository.Remove(rent);
        await _unitOfWork.SaveChangesAsync();
        
        return null;
    }
}
