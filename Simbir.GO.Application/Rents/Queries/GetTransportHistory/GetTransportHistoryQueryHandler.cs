using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Queries.GetTransportHistory;

internal class GetTransportHistoryQueryHandler : IRequestHandler<GetTransportHistoryQuery, ErrorOr<List<RentResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransportHistoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<RentResult>>> Handle(GetTransportHistoryQuery request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        if (transport.OwnerId != request.UserId)
            return Errors.Transport.OnlyOwnerCanDealWithHisTransport;

        var rents = await _unitOfWork.RentRepository.GetRentsByTransportId(request.TransportId);

        return rents.Select(x => new RentResult(x.Id, x.RenterId, x.TransportId, x.RentType.ToString(),
            x.TimeStart, x.TimeEnd, x.PriceOfUnit, x.FinalPrice)).ToList();
    }
}
