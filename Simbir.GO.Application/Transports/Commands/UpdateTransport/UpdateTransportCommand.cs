using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.Transports.Commands.UpdateTransport;

public record UpdateTransportCommand(int UserId, int TransportId, bool CanBeRented, string Model,
    string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice) : IRequest<ErrorOr<TransportResult>>;