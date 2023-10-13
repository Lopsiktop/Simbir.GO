using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Application.Transports.Queries.GetTransport;

internal class GetTransportQueryHandler : IRequestHandler<GetTransportQuery, ErrorOr<TransportResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TransportResult>> Handle(GetTransportQuery request, CancellationToken cancellationToken)
    {
        var transport = await _unitOfWork.TransportRepository.FindById(request.Id);

        if (transport is null)
            return Errors.Transport.TransportDoesNotExist;

        return new TransportResult(
            transport.Id,
            transport.OwnerId,
            transport.CanBeRented,
            transport.TransportType.ToString(),
            transport.Model,
            transport.Color,
            transport.Identifier,
            transport.Description,
            transport.Latitude,
            transport.Longitude,
            transport.MinutePrice,
            transport.DayPrice);
    }
}
