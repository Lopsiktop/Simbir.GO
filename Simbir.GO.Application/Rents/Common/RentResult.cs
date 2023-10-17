namespace Simbir.GO.Application.Rents.Common;

public record RentResult(int Id, int RenterId, int TransportId, string RentType, DateTime TimeStart,
    DateTime? TimeEnd, double PriceOfUnit, double? FinalPrice);