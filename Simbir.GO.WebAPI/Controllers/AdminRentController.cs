using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Rents.Queries.GetHistory;
using Simbir.GO.Application.Rents.Queries.GetRent;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Infrastructure.Identity;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Admin")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class AdminRentController : ApiContoller
{
    private readonly IMediator _mediator;

    public AdminRentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Rent/{rentId}")]
    public async Task<IActionResult> GetRent(int rentId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetRentAdminQuery(GetUserId(), rentId);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("UserHistory/{userId}")]
    public async Task<IActionResult> GetHistory(int userId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetHistoryQuery(userId);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}
