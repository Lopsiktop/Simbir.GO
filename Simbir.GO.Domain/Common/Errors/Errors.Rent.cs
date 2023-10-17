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
    }
}
