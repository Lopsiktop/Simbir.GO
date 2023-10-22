using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;

namespace Simbir.GO.Application.Accounts.Commands.Update;

public record UpdateAccountCommand(int UserId, string Username, string Password)
    : IRequest<ErrorOr<AccountResult>>;