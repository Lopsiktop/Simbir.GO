using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;

namespace Simbir.GO.Application.AdminAccounts.Queries.GetAccount;

public record GetAccountAdminQuery(int UserId) : IRequest<ErrorOr<AccountAdminResult>>;