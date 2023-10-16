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

    public static ErrorOr<TransportType> ToTransportType(string type)
    {
        type = type.ToLower();
        ErrorOr<TransportType> transportType = type switch {
            "car" => TransportType.Car,
            "bike" => TransportType.Bike,
            "scooter" => TransportType.Scooter,
            _ => Errors.Transport.TransportTypeDoesNotExist 
        };

        return transportType;
    }

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

    public List<Error> Update(Account owner, bool canBeRented, string transportType, string model, string color, string identifier, string? description, double latitude, double longitude, double? minutePrice, double? dayPrice)
    {
        var result = Update(canBeRented, model, color, identifier, description, latitude, longitude, minutePrice, dayPrice);
        if (result.Count != 0)
            return result;

        var typeResult = ToTransportType(transportType);
        if (typeResult.IsError)
            return typeResult.Errors;

        Owner = owner;
        OwnerId = owner.Id;
        TransportType = typeResult.Value;

        return result;
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

    public static ErrorOr<Transport> Create(Account owner, bool canBeRented, string transportType, string model, string color, string identifier, string? description, double latitude, double longitude, double? minutePrice, double? dayPrice)
    {
        var validation = Validate(owner, model, color, identifier);
        if (validation.Count != 0)
            return validation;

        var type = ToTransportType(transportType);

        if (type.IsError)
            return type.Errors;

        return new Transport(owner, canBeRented, type.Value, model, color, identifier, description, latitude, longitude, minutePrice, dayPrice);
    }
}