using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;

namespace Simbir.GO.Application.Accounts.Queries.SingIn;

public record SignInAccountQuery(string Username, string Password) : IRequest<ErrorOr<TokenResult>>;