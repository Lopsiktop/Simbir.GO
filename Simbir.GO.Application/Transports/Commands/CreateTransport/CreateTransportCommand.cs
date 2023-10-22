using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.Transports.Commands.CreateTransport;

public record CreateTransportCommand(int OwnerId, bool CanBeRented, string TransportType, string Model,
    string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice) : IRequest<ErrorOr<TransportResult>>;