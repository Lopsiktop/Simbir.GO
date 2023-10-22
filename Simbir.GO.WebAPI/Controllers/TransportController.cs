using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminTransports.Commands.DeleteTransport;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Application.Transports.Commands.DeleteTransport;
using Simbir.GO.Application.Transports.Commands.UpdateTransport;
using Simbir.GO.Application.Transports.Common;
using Simbir.GO.Application.Transports.Queries;
using Simbir.GO.Application.Transports.Queries.GetTransport;
using Simbir.GO.Contracts.TransportContracts;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Transport")]
public class TransportController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TransportController(IMediator mediator, IMapper mapper, ICheckAccounts accounts) : base(accounts)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransport(int id)
    {
        var query = new GetTransportQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransport(TransportRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = _mapper.Map<CreateTransportCommand>((GetUserId(), request));
        var result = await _mediator.Send(command);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransport(int id, UpdateTransportRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = _mapper.Map<UpdateTransportCommand>((GetUserId(), id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransport(int id)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new DeleteTransportCommand(id, GetUserId());
        var result = await _mediator.Send(command);

        if (result.HasValue)
            return Problem(result.Value);

        return NoContent();
    }
}
