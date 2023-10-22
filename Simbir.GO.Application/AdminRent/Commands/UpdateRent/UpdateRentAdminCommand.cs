using ErrorOr;
using MediatR;
using Simbir.GO.Application.Rents.Common;

namespace Simbir.GO.Application.AdminRent.Commands.NewRent;


public record UpdateRentAdminCommand(int RentId, int TransportId, int UserId, string TimeStart, string? TimeEnd, double PriceOfUnit, string PriceType, double? FinalPrice)
    : IRequest<ErrorOr<RentResult>>;