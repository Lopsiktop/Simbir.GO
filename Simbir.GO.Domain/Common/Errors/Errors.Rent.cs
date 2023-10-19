using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Rent
    {
        public static Error RentTypeDoesNotExist =>
            Error.NotFound("Rent.RentTypeDoesNotExist", "Такой тип аренды несуществует!");

        public static Error RentDoesNotExist =>
            Error.NotFound("Rent.RentDoesNotExist", "Аренда с таким id несуществует!");

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

        public static Error OnlyRenterOrTransportOwnerCanDealWithTheirRents =>
            Error.Conflict("Rent.OnlyRenterOrTransportOwnerCanDealWithTheirRents", "Только арендатор или владелец транспорта могут взаимодействовать с их арендами!");

        public static Error OnlyRenterCanEndHisRent =>
            Error.Conflict("Rent.OnlyRenterCanEndHisRent", "Только арендатор может завершить аренду!");

        public static Error ThisRentHasAlreadyFinished =>
            Error.Conflict("Rent.ThisRentHasAlreadyFinished", "Эта аренда уже завершена!");
    }
}
