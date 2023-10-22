namespace Simbir.GO.Contracts.TransportContracts;

public record UpdateTransportRequest(bool CanBeRented, string Model,
    string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice);