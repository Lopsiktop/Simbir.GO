using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.AdminTransports.Queries.GetAllTransport;

public record GetAllTransportQuery(int Start, int Count, string TransportType) :
    IRequest<ErrorOr<List<TransportResult>>>;