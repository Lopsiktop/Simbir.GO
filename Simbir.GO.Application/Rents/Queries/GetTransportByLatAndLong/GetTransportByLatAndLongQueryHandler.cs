using ErrorOr;
using MediatR;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Domain.TransportEntity;

namespace Simbir.GO.Application.Rents.Queries.GetTransportByLatAndLong;

internal class GetTransportByLatAndLongQueryHandler : IRequestHandler<GetTransportByLatAndLongQuery, ErrorOr<List<TransportResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTransportByLatAndLongQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<TransportResult>>> Handle(GetTransportByLatAndLongQuery request, CancellationToken cancellationToken)
    {
        var typeResult = Transport.ToTransportType(request.Type);
        if (typeResult.IsError && request.Type.ToLower() != "all")
            return typeResult.Errors;

        TransportType? type = request.Type.ToLower() == "all" ? null : typeResult.Value;

        var transports = await _unitOfWork.RentRepository.GetTransportsByLatAndLong(request.Lat, request.Long, request.Radius, type);

        return transports.Select(x => new TransportResult(
            x.Id,
            x.OwnerId,
            x.CanBeRented,
            x.TransportType.ToString(),
            x.Model,
            x.Color,
            x.Identifier,
            x.Description,
            x.Latitude,
            x.Longitude,
            x.MinutePrice,
            x.DayPrice)).ToList();
    }
}
