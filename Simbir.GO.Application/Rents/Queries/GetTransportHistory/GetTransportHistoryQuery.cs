using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Queries.GetTransportHistory;

public record GetTransportHistoryQuery(int TransportId, int UserId)
    : IRequest<ErrorOr<List<RentResult>>>;