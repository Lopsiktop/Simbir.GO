using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;

namespace Simbir.GO.Application.Accounts.Queries.GetMe;

public record GetMeQuery(int UserId) : IRequest<ErrorOr<AccountResult>>;