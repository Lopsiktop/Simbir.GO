using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Queries.GetHistory;

public record GetHistoryQuery(int UserId) : IRequest<ErrorOr<List<RentResult>>>;