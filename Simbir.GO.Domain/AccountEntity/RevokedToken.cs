using ErrorOr;
using Simbir.GO.Domain.Base;
using Simbir.GO.Domain.Common.Errors;

namespace Simbir.GO.Domain.AccountEntity;

public class RevokedToken : Entity
{
    public string Token { get; private set; }

    private RevokedToken(string token)
    {
        Token = token;
    }

    public static ErrorOr<RevokedToken> Create(string token)
    {
        if (string.IsNullOrEmpty(token))
            return Errors.Account.TokenCannotBeEmpty;

        return new RevokedToken(token);
    }
}
