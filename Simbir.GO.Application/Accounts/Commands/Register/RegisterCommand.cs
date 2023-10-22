using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;

namespace Simbir.GO.Application.Accounts.Commands.Register;

public record RegisterCommand(string Username, string Password) : IRequest<ErrorOr<AccountResult>>;