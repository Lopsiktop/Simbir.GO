using ErrorOr;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.Base;
using Simbir.GO.Domain.Common.Errors;
using Simbir.GO.Domain.TransportEntity;
using System.Runtime.CompilerServices;

namespace Simbir.GO.Domain.RentEntity;

public class Rent : Entity
{
    public Account Renter { get; private set; } = null!;
    public int RenterId { get; private set; }

    public Transport Transport { get; private set; } = null!;
    public int TransportId { get; private set; }

    public RentType RentType { get; private set; }

    public DateTime TimeStart { get; private set; }

    public DateTime? TimeEnd { get; private set; }

    public double PriceOfUnit { get; private set; }

    public double? FinalPrice { get; private set; }

    private Rent(Account renter, Transport transport, RentType rentType, DateTime timeStart, DateTime? timeEnd, double priceOfUnit, double? finalPrice)
    {
        Renter = renter;
        RenterId = renter.Id;
        Transport = transport;
        TransportId = transport.Id;
        RentType = rentType;
        TimeStart = timeStart;
        TimeEnd = timeEnd;
        PriceOfUnit = priceOfUnit;
        FinalPrice = finalPrice;
    }

    private Rent() { }

    public static ErrorOr<RentType> ToRentType(string type)
    {
        type = type.ToLower();
        ErrorOr<RentType> typeResult = type switch
        {
            "minutes" => RentType.Minutes,
            "days" => RentType.Days,
            _ => Errors.Rent.RentTypeDoesNotExist
        };

        return typeResult;
    }

    public Error? EndRent()
    {
        if (TimeEnd is not null)
            return Errors.Rent.ThisRentHasAlreadyFinished;

        TimeEnd = DateTime.UtcNow;
        double? units = null;

        var duration = TimeEnd - TimeStart;

        if (RentType == RentType.Days)
            units = duration.HasValue ? duration.Value.TotalDays : null;
        else if (RentType == RentType.Minutes)
            units = duration.HasValue ? duration.Value.TotalMinutes : null;

        if (units is null)
            return null;

        double amount = Math.Round(units.Value);
        FinalPrice = PriceOfUnit * amount;
        return null;
    }

    public static List<Error> Validate(Account renter, Transport transport, DateTime timeStart, DateTime? timeEnd, double? priceOfUnit, double? finalPrice)
    {
        var errors = new List<Error>();

        if (renter is null)
            errors.Add(Errors.Account.AccountDoesNotExist);
        if (transport is null)
            errors.Add(Errors.Transport.TransportDoesNotExist);
        if (timeStart > timeEnd)
            errors.Add(Errors.Rent.TimeStartMustBeLessThenTimeEnd);
        if(priceOfUnit.HasValue && priceOfUnit.Value < 0)
            errors.Add(Errors.Rent.PriceOfUnitCannotBeLessThenZero);
        if (finalPrice.HasValue && finalPrice.Value < 0)
            errors.Add(Errors.Rent.FinalPriceCannotBeLessThenZero);
        if (!transport!.CanBeRented)
            errors.Add(Errors.Transport.ThisTransportCannotBeRented);
        if (transport.OwnerId == renter!.Id)
            errors.Add(Errors.Rent.OwnerCannotRentHisOwnTransport);

        return errors;
    }

    public static ErrorOr<Rent> Create(Account renter, Transport transport, string rentType, DateTime timeStart, DateTime? timeEnd, double? priceOfUnit, double? finalPrice)
    {
        var validation = Validate(renter, transport, timeStart, timeEnd, priceOfUnit, finalPrice);
        if (validation.Count != 0)
            return validation;

        var rentTypeResult = ToRentType(rentType);
        if (rentTypeResult.IsError)
            return rentTypeResult.Errors;

        var type = rentTypeResult.Value;

        if(priceOfUnit is null)
            priceOfUnit = type switch
            {
                RentType.Minutes => transport.MinutePrice,
                RentType.Days => transport.DayPrice
            };

        if (priceOfUnit is null)
            return Errors.Rent.ThePriceOfUnitForTransportIsNotIndicated;

        return new Rent(renter, transport, type, timeStart, timeEnd, (double)priceOfUnit, finalPrice);
    }
}
