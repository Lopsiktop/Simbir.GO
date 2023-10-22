using ErrorOr;
using MediatR;
using Simbir.GO.Application.Accounts.Common;

namespace Simbir.GO.Application.Payment.Commands.AddMoney;

public record AddMoneyCommand(int FromUserId, int ToUserId) : IRequest<ErrorOr<AccountResult>>;