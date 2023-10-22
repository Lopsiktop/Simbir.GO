using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.RentEntity;

namespace Simbir.GO.Application.AdminRent.Commands.NewRent;

internal class UpdateRentAdminCommandHandler : IRequestHandler<UpdateRentAdminCommand, ErrorOr<RentResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRentAdminCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RentResult>> Handle(UpdateRentAdminCommand request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        var user = await _unitOfWork.AccountRepository.FindById(request.UserId);
        if (user is null)
            return Errors.Account.AccountDoesNotExist;

        var rent = await _unitOfWork.RentRepository.FindById(request.RentId);
        if (rent is null)
            return Errors.Rent.RentDoesNotExist;

        if (!DateTime.TryParse(request.TimeStart, out var timeStart))
            return Errors.Rent.TimeStartIsWrong;

        var timeEndResult = DateTime.TryParse(request.TimeEnd, out var timeEnd);

        if (!timeEndResult && request.TimeEnd is not null)
            return Errors.Rent.TimeEndIsWrong;

        var updateResult = rent.Update(transport, user, timeStart.ToUniversalTime(),
            timeEndResult ? timeEnd.ToUniversalTime() : null,
            request.PriceOfUnit, request.PriceType, request.FinalPrice);

        if (updateResult.Count != 0)
            return updateResult;

        await _unitOfWork.SaveChangesAsync();

        return new RentResult(rent.Id, rent.RenterId, rent.TransportId, rent.RentType.ToString(),
            rent.TimeStart, rent.TimeEnd, rent.PriceOfUnit, rent.FinalPrice);
    }
}
