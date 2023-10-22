using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Rents.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Rents.Queries.GetTransportHistory;

internal class GetTransportHistoryAdminQueryHandler : IRequestHandler<GetTransportHistoryAdminQuery, ErrorOr<List<RentResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransportHistoryAdminQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<RentResult>>> Handle(GetTransportHistoryAdminQuery request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.TransportId);
        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        var rents = await _unitOfWork.RentRepository.GetRentsByTransportId(request.TransportId);

        return rents.Select(x => new RentResult(x.Id, x.RenterId, x.TransportId, x.RentType.ToString(),
            x.TimeStart, x.TimeEnd, x.PriceOfUnit, x.FinalPrice)).ToList();
    }
}
