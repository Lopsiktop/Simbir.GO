using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.Transports.Queries.GetTransport;

public record GetTransportQuery(int Id) : IRequest<ErrorOr<TransportResult>>;