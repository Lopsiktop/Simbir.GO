using ErrorOr;
using Simbir.GO.Domain.Base;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Domain.AccountEntity;

public class RevokedToken : Entity
{
    public string Token { get; private set; }

    public DateTime ExpirationTimeUtc { get; private set; }

    private RevokedToken(string token, DateTime expirationTimeUtc = default)
    {
        Token = token;
        ExpirationTimeUtc = expirationTimeUtc;
    }

    public static Error? Validate(string token)
    {
        if (string.IsNullOrEmpty(token))
            return Errors.Account.TokenCannotBeEmpty;

        return null;
    }

    public static ErrorOr<RevokedToken> Create(string token, DateTime expirationTimeUtc)
    {
        var validate = Validate(token);
        if (validate.HasValue)
            return validate.Value;

        return new RevokedToken(token, expirationTimeUtc);
    }
}
