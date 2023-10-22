using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error UsernameMustBeUnique =>
            Error.Conflict("Account.UsernameMustBeUnique", "Пользователь с таким именем уже зарегистрирован!");

        public static Error AccountDoesNotExist =>
            Error.NotFound("Account.AccountDoesNotExist", "Такого пользователя несуществует! Неверно указан логин или пароль!");

        public static Error UsernameCannotBeEmpty =>
            Error.Validation("Account.UsernameCannotBeEmpty", "Поле 'username' не может быть пустым!");

        public static Error PasswordCannotBeEmpty =>
            Error.Validation("Account.PasswordCannotBeEmpty", "Поле 'password' не может быть пустым!");

        public static Error BalanceCannotBeNegative =>
            Error.Validation("Account.BalanceCannotBeNegative", "Поле 'balance' не может быть отрицательным!");

        public static Error TokenCannotBeEmpty =>
            Error.Validation("Account.TokenCannotBeEmpty", "Поле 'token' не может быть пустым!");
    }
}
