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
    }
}
