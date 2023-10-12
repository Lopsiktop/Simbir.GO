using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class General
    {
        public static Error StartNumberCannotBeNegative =>
            Error.Validation("General.StartNumberCannotBeNegative", "Поле 'start' не может быть отрицательным!");

        public static Error CountNumberCannotBeLessThanZero =>
            Error.Validation("General.StartNumberCannotBeNegative", "Поле 'count' не может быть меньше нуля!");
    }
}
