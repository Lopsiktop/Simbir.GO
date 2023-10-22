using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Queries.GetRent;

public record GetRentQuery(int UserId, int RentId) : IRequest<ErrorOr<RentResult>>;