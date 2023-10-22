using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Commands.EndRent;

internal class EndRentCommandHandler : IRequestHandler<EndRentCommand, ErrorOr<RentResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public EndRentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RentResult>> Handle(EndRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _unitOfWork.RentRepository.FindByIdInclude(request.RentId);
        if (rent is null)
            return Errors.Rent.RentDoesNotExist;

        if (rent.RenterId != request.UserId)
            return Errors.Rent.OnlyRenterCanEndHisRent;

        rent.Transport.UpdateLocation(request.Lat, request.Long);

        var result = rent.EndRent();
        if (result.HasValue)
            return result.Value;

        await _unitOfWork.SaveChangesAsync();

        return new RentResult(rent.Id, rent.RenterId, rent.TransportId, rent.RentType.ToString(),
            rent.TimeStart, rent.TimeEnd, rent.PriceOfUnit, rent.FinalPrice);
    }
}
