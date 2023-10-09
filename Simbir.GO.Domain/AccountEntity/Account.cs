using Simbir.GO.Domain.Base;

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

    public static Account Create(string username, string password)
    {
        string passwordHash = Crypt.EnhancedHashPassword(password, 13);
        return new Account(username, 0, false, passwordHash);
    }

    public static Account CreateByAdmin(string username, string password, bool isAdmin, double balance)
    {
        string passwordHash = Crypt.EnhancedHashPassword(password, 13);
        return new Account(username, balance, isAdmin, passwordHash);
    }
}
