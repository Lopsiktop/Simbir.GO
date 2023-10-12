using ErrorOr;
using MediatR;
using Simbir.GO.Application.AdminAccounts.Common;

namespace Simbir.GO.Application.AdminAccounts.Queries;

public record GetAllAccountsAdminQuery(int Start, int Count)
    : IRequest<ErrorOr<List<AccountAdminResult>>>;