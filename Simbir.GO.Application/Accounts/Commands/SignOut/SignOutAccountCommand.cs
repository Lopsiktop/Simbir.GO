using ErrorOr;
using MediatR;

namespace Simbir.GO.Application.Accounts.Commands.SignOut;

public record SignOutAccountCommand(string Token, string ExpirationTimeUtc) : IRequest<Error?>;