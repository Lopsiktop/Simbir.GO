using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Application.AdminTransports.Queries.GetAllTransport;

internal class GetAllTransportQueryHandler : IRequestHandler<GetAllTransportQuery, ErrorOr<List<TransportResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTransportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<TransportResult>>> Handle(GetAllTransportQuery request, CancellationToken cancellationToken)
    {
        if (request.Start < 0)
            return Errors.General.StartNumberCannotBeNegative;

        if (request.Count <= 0)
            return Errors.General.CountNumberCannotBeLessThanZero;

        var transportTypeResult = Transport.ToTransportType(request.TransportType);

        if (transportTypeResult.IsError)
            return transportTypeResult.Errors;

        var transportType = transportTypeResult.Value;

        var transports = await _unitOfWork.TransportRepository.GetAllTransports(request.Start, request.Count, transportType);

        return transports.Select(x => new TransportResult(x.Id, x.OwnerId, x.CanBeRented, x.TransportType.ToString(), x.Model, x.Color, x.Identifier, x.Description, x.Latitude, x.Longitude, x.MinutePrice, x.DayPrice))
            .ToList();
    }
}
