using ErrorOr;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Domain.Base;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Domain.TransportEntity;

public class Transport : Entity
{
    public Account? Owner { get; private set; } = null;
    public int OwnerId { get; private set; }

    public bool CanBeRented { get; private set; }

    public TransportType TransportType { get; private set; }

    public string Model { get; private set; }

    public string Color { get; private set; }

    public string Identifier { get; private set; } 

    public string? Description { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public double? MinutePrice { get; private set; }

    public double? DayPrice { get; private set; }

    private Transport(Account owner, bool canBeRented, TransportType transportType, string model, string color, string identifier, string? description, double latitude, double longitude, double? minutePrice, double? dayPrice)
    {
        Owner = owner;
        OwnerId = owner.Id;
        CanBeRented = canBeRented;
        TransportType = transportType;
        Model = model;
        Color = color;
        Identifier = identifier;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        MinutePrice = minutePrice;
        DayPrice = dayPrice;
    }

    private Transport() { }

    public static List<Error> Validate(Account owner, string model, string color, string identifier)
    {
        var errors = new List<Error>();

        if (owner is null)
            errors.Add(Errors.Transport.OwnerCannotBeNull);
        if (string.IsNullOrEmpty(model))
            errors.Add(Errors.Transport.ModelCannotBeEmpty);
        if (string.IsNullOrEmpty(color))
            errors.Add(Errors.Transport.ColorCannotBeEmpty);
        if (string.IsNullOrEmpty(identifier))
            errors.Add(Errors.Transport.IdentifierCannotBeEmpty);

        return errors;
    }

    public List<Error> Update(bool canBeRented, string model, string color, string identifier, string? description, double latitude, double longitude, double? minutePrice, double? dayPrice)
    {
        var validation = Validate(Account.Empty(), model, color, identifier);
        if (validation.Count != 0)
            return validation;

        CanBeRented = canBeRented;
        Model = model;
        Color = color;
        Identifier = identifier;
        Description = description;
        Latitude = latitude;
        Longitude = longitude;
        MinutePrice = minutePrice;
        DayPrice = dayPrice;

        return validation;
    }

    public static ErrorOr<Transport> Create(Account owner, bool canBeRented, TransportType transportType, string model, string color, string identifier, string? description, double latitude, double longitude, double? minutePrice, double? dayPrice)
    {
        var validation = Validate(owner, model, color, identifier);
        if (validation.Count != 0)
            return validation;

        return new Transport(owner, canBeRented, transportType, model, color, identifier, description, latitude, longitude, minutePrice, dayPrice);
    }
}