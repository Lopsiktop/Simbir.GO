namespace Simbir.GO.Application.Transports.Common;

public record TransportResult(int Id, int OwnerId, bool CanBeRented, string TransportType, string Model,
    string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice);
