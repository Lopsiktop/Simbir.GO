using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Rents.Commands.EndRent;
using Simbir.GO.Application.Rents.Commands.NewRent;
using Simbir.GO.Application.Rents.Queries.GetHistory;
using Simbir.GO.Application.Rents.Queries.GetRent;
using Simbir.GO.Application.Rents.Queries.GetTransportByLatAndLong;
using Simbir.GO.Application.Rents.Queries.GetTransportHistory;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Contracts.TransportContracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Rent")]
public class RentController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RentController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("Transport")]
    public async Task<IActionResult> GetTransportsBeLatAndLong(double lat, double longitude, double radius, string type)
    {
        var query = new GetTransportByLatAndLongQuery(lat, longitude, radius, type);
        var result = await _mediator.Send(query);

        return result.Match(
            transports => Ok(transports),
            errors => Problem(errors));
    }

    [HttpGet("{rentId}")]
    public async Task<IActionResult> GetRentById(int rentId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new GetRentQuery(GetUserId(), rentId);
        var result = await _mediator.Send(command);

        return result.Match(
            rent => Ok(rent),
            errors => Problem(errors));
    }

    [HttpGet("MyHistory")]
    public async Task<IActionResult> GetMyHistory()
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new GetHistoryQuery(GetUserId());
        var result = await _mediator.Send(command);

        return result.Match(
            history => Ok(history),
            errors => Problem(errors));
    }

    [HttpGet("TransportHistory/{transportId}")]
    public async Task<IActionResult> GetTransportHistory(int transportId)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new GetTransportHistoryQuery(transportId, GetUserId());
        var result = await _mediator.Send(command);

        return result.Match(
            history => Ok(history),
            errors => Problem(errors));
    }

    [HttpPost("New/{transportId}")]
    public async Task<IActionResult> NewRent(int transportId, string rentType)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new NewRentCommand(transportId, GetUserId(), rentType);
        var result = await _mediator.Send(command);

        return result.Match(
            rent => Ok(rent),
            errors => Problem(errors));
    }

    [HttpPost("End/{rentId}")]
    public async Task<IActionResult> EndRent(int rentId, double latitude, double longitude)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new EndRentCommand(rentId, GetUserId(), latitude, longitude);
        var result = await _mediator.Send(command);

        return result.Match(
            rent => Ok(rent),
            errors => Problem(errors));
    }
}
