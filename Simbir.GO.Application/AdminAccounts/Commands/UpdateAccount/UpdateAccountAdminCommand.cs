using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;

namespace Simbir.GO.Application.AdminAccounts.Commands.UpdateAccount;

public record UpdateAccountAdminCommand(int UserId, string Username, string Password, bool IsAdmin, double Balance)
    : IRequest<ErrorOr<AccountAdminResult>>;