using MediatR;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Payment.Commands.AddMoney;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Payment")]
public class PaymentController : ApiContoller
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator, ICheckAccounts checkAccounts) : base(checkAccounts)
    {
        _mediator = mediator;
    }

    [HttpPost("Hesoyam/{accountId}")]
    public async Task<IActionResult> Hesoyam(int accountId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new AddMoneyCommand(GetUserId(), accountId);
        var result = await _mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
