﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Rents.Commands.NewRent;
using Simbir.GO.Application.Rents.Queries.GetTransportByLatAndLong;
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
}
