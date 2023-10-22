using ErrorOr;
using MediatR;

namespace Simbir.GO.Application.AdminAccounts.Commands.DeleteAccount;

public record DeleteAccountAdminCommand(int UserId) : IRequest<Error?>;