using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminRent.Commands.NewRent;
using Simbir.GO.Application.Rents.Commands.EndRent;
using Simbir.GO.Application.Rents.Queries.GetHistory;
using Simbir.GO.Application.Rents.Queries.GetRent;
using Simbir.GO.Application.Rents.Queries.GetTransportHistory;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Contracts.RentContracts;
using Simbir.GO.Domain.RentEntity;
using Simbir.GO.Infrastructure.Identity;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Admin")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class AdminRentController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminRentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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

    [HttpGet("TransportHistory/{transportId}")]
    public async Task<IActionResult> GetTransportHistory(int transportId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetTransportHistoryAdminQuery(transportId);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost("Rent")]
    public async Task<IActionResult> NewRent(RentAdminRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = _mapper.Map<NewRentAdminCommand>(request);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost("Rent/End/{rentId}")]
    public async Task<IActionResult> EndRent(int rentId, double latitude, double longitude)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new EndRentAdminCommand(rentId, GetUserId(), latitude, longitude);
        var result = await _mediator.Send(command);

        return result.Match(Ok, Problem);
    }
}
