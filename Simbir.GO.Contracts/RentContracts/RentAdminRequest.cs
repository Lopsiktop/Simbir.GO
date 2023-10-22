namespace Simbir.GO.Contracts.RentContracts;

public record RentAdminRequest(int TransportId,
                               int UserId,
                               string TimeStart,
                               string? TimeEnd,
                               double PriceOfUnit,
                               string PriceType,
                               double? FinalPrice);