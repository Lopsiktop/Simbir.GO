using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Queries.GetRent;

internal class GetRentQueryHandler : IRequestHandler<GetRentQuery, ErrorOr<RentResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RentResult>> Handle(GetRentQuery request, CancellationToken cancellationToken)
    {
        var rent = await _unitOfWork.RentRepository.FindByIdInclude(request.RentId);

        if (rent is null)
            return Errors.Rent.RentDoesNotExist;

        if (rent.RenterId != request.UserId && rent.Transport?.OwnerId != request.UserId)
            return Errors.Rent.OnlyRenterOrTransportOwnerCanDealWithTheirRents;

        return new RentResult(rent.Id, rent.RenterId, rent.TransportId, rent.RentType.ToString(),
            rent.TimeStart, rent.TimeEnd, rent.PriceOfUnit, rent.FinalPrice);
    }
}
