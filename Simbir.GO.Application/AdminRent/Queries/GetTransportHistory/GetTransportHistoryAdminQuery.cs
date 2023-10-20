using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Queries.GetTransportHistory;

public record GetTransportHistoryAdminQuery(int TransportId)
    : IRequest<ErrorOr<List<RentResult>>>;