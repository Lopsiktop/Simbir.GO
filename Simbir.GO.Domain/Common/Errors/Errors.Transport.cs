using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Transport
    {
        public static Error OwnerCannotBeNull =>
            Error.Validation("Transport.OwnerCannotBeNull", "Owner не может быть null!");

        public static Error ModelCannotBeEmpty =>
            Error.Validation("Transport.ModelCannotBeEmpty", "Поле 'model' не может быть пустым!");

        public static Error ColorCannotBeEmpty =>
            Error.Validation("Transport.ColorCannotBeEmpty", "Поле 'color' не может быть пустым!");

        public static Error IdentifierCannotBeEmpty =>
            Error.Validation("Transport.IdentifierCannotBeEmpty", "Поле 'identifier' не может быть пустым!");

        public static Error TransportDoesNotExist =>
            Error.Validation("Transport.TransportDoesNotExist", "Транспорт с таким id несуществует!");

        public static Error TransportTypeDoesNotExist =>
            Error.Validation("Transport.TransportTypeDoesNotExist", "Такой тип транспорта несуществует!");
        
        public static Error OnlyOwnerCanDealWithHisTransport =>
            Error.Conflict("Transport.OnlyOwnerCanDealWithHisTransport", "Только владелец транспорта может с ним работать!");

        public static Error ThisTransportCannotBeRented =>
            Error.Conflict("Transport.ThisTransportCannotBeRented", "Этот транспорт не может быть арендован, так решил владелец!");
    }
}
