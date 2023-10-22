namespace Simbir.GO.Contracts.TransportContracts;

public record AdminTransportRequest(int OwnerId, bool CanBeRented, string TransportType, string Model,
    string Color, string Identifier, string? Description, double Latitude, double Longitude,
    double? MinutePrice, double? DayPrice);