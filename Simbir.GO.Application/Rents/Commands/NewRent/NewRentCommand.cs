using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.Rents.Commands.NewRent;

public record NewRentCommand(int TransportId, int RenterId, string RentType)
    : IRequest<ErrorOr<RentResult>>;
