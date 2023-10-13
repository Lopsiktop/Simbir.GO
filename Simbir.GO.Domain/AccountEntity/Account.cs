using ErrorOr;
using Simbir.GO.Domain.Base;
using Simbir.GO.Domain.Common.Errors;
using Crypt = BCrypt.Net.BCrypt;

namespace Simbir.GO.Domain.AccountEntity;

public class Account : Entity
{
    public string Username { get; private set; }

    public double Balance { get; private set; }

    public bool IsAdmin { get; private set; }

    public string PasswordHash { get; private set; }

    private Account(string username, double balance, bool isAdmin, string passwordHash)
    {
        Username = username;
        Balance = balance;
        IsAdmin = isAdmin;
        PasswordHash = passwordHash;
    }

    public bool VerifyPassword(string password)
    {
        bool answer = Crypt.EnhancedVerify(password, PasswordHash);
        return answer;
    }

    public List<Error> Update(string username, string password)
    {
        var errors = ValidateAccount(username, password);

        if (errors.Count != 0)
            return errors;

        string passwordHash = Crypt.EnhancedHashPassword(password, 13);

        Username = username;
        PasswordHash = passwordHash;

        return errors;
    }

    public List<Error> Update(string username, string password, bool isAdmin, double balance)
    {
        var errors = ValidateAccount(username, password, balance);

        if (errors.Count != 0)
            return errors;

        string passwordHash = Crypt.EnhancedHashPassword(password, 13);

        Username = username;
        PasswordHash = passwordHash;
        IsAdmin = isAdmin;
        Balance = balance;

        return errors;
    }

    public static List<Error> ValidateAccount(string username, string password = "default", double balance = default)
    {
        List<Error> errors = new List<Error>();

        if (string.IsNullOrEmpty(username))
            errors.Add(Errors.Account.UsernameCannotBeEmpty);

        if (string.IsNullOrEmpty(password))
            errors.Add(Errors.Account.PasswordCannotBeEmpty);

        if(balance < 0)
            errors.Add(Errors.Account.BalanceCannotBeNegative);

        return errors;
    }

    public static ErrorOr<Account> Create(string username, string password, bool isAdmin = false, double balance = 0)
    {
        var errors = ValidateAccount(username, password);

        if (errors.Count != 0)
            return errors;

        string passwordHash = Crypt.EnhancedHashPassword(password, 13);
        return new Account(username, balance, isAdmin, passwordHash);
    }
}
