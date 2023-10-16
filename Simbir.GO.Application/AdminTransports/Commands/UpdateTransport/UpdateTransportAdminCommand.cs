using ErrorOr;
using MediatR;
using Simbir.GO.Application.Transports.Common;

namespace Simbir.GO.Application.AdminTransports.Commands.UpdateTransport;

public record UpdateTransportAdminCommand(int TransportId, int OwnerId, bool CanBeRented, string TransportType,
    string Model, string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice) : IRequest<ErrorOr<TransportResult>>;