using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Transport
    {
        public static Error OwnerCannotBeNull =>
            Error.Conflict("Transport.OwnerCannotBeNull", "Owner не может быть null!");

        public static Error ModelCannotBeEmpty =>
            Error.Conflict("Transport.ModelCannotBeEmpty", "Поле 'model' не может быть пустым!");

        public static Error ColorCannotBeEmpty =>
            Error.Conflict("Transport.ColorCannotBeEmpty", "Поле 'color' не может быть пустым!");

        public static Error IdentifierCannotBeEmpty =>
            Error.Conflict("Transport.IdentifierCannotBeEmpty", "Поле 'identifier' не может быть пустым!");

        public static Error TransportDoesNotExist =>
            Error.Conflict("Transport.TransportDoesNotExist", "Транспорт с таким id несуществует!");

        public static Error TransportTypeDoesNotExist =>
            Error.Conflict("Transport.TransportDoesNotExist", "Такой тип транспорта несуществует!");
    }
}
