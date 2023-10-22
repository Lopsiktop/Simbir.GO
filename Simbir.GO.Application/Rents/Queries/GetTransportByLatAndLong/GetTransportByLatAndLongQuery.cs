using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.Rents.Queries.GetTransportByLatAndLong;

public record GetTransportByLatAndLongQuery(double Lat, double Long, double Radius, string Type) 
    : IRequest<ErrorOr<List<TransportResult>>>;