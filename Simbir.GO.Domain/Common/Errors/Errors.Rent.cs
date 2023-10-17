using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Rent
    {
        public static Error RentTypeDoesNotExist =>
            Error.Validation("Rent.RentTypeDoesNotExist", "Такой тип аренды несуществует!");

        public static Error TimeStartMustBeLessThenTimeEnd =>
            Error.Validation("Rent.TimeStartMustBeLessThenTimeEnd", "Время начала аренды должно быть меньше время его конца!");

        public static Error PriceOfUnitCannotBeLessThenZero =>
            Error.Validation("Rent.PriceOfUnitCannotBeLessThenZero", "Цена за единицу времени не может быть меньше нуля!");

        public static Error FinalPriceCannotBeLessThenZero =>
            Error.Validation("Rent.FinalPriceCannotBeLessThenZero", "Финальная цена не может быть меньше нуля!");

        public static Error ThisTransportHasAlreadyRented =>
            Error.Conflict("Rent.ThisTransportHasAlreadyRented", "Этот транспорт уже арендован!");

        public static Error OwnerCannotRentHisOwnTransport =>
            Error.Conflict("Rent.OwnerCannotRentHisOwnTransport", "Владелец не может арендовать свой транспорт!");

        public static Error ThePriceOfUnitForTransportIsNotIndicated =>
            Error.Conflict("Rent.ThePriceOfUnitForTransportIsNotIndicated", "Вы не можете арендовать транспорт, потому что владелец не указал цены. Попробуйте другой тип аренды!");
    }
}
