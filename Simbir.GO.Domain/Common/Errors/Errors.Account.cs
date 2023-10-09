using ErrorOr;

namespace Simbir.GO.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error UsernameMustBeUnique =>
            Error.Conflict("Account.UsernameMustBeUnique", "Пользователь с таким именем уже зарегистрирован!");
    }
}
