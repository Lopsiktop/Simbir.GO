using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminTransports.Commands.DeleteTransport;
using Simbir.GO.Application.AdminTransports.Commands.UpdateTransport;
using Simbir.GO.Application.AdminTransports.Queries.GetAllTransport;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
using Simbir.GO.Application.Transports.Commands.DeleteTransport;
using Simbir.GO.Application.Transports.Commands.UpdateTransport;
using Simbir.GO.Application.Transports.Queries.GetTransport;
using Simbir.GO.Contracts.TransportContracts;
using Simbir.GO.Infrastructure.Identity;

namespace Simbir.GO.WebAPI.Controllers;

[Route("/api/Admin/Transport")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class AdminTransportController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminTransportController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransports(int start, int count, string transportType)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetAllTransportQuery(start, count, transportType);
        var result = await _mediator.Send(query);

        return result.Match(
            transports => Ok(transports),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransport(int id)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetTransportQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransport(AdminTransportRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = _mapper.Map<CreateTransportCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransport(int id, UpdateTransportAdminRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = _mapper.Map<UpdateTransportAdminCommand>((id, request));
        var result = await _mediator.Send(command);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransport(int id)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new DeleteTransportAdminCommand(id);
        var result = await _mediator.Send(command);

        if (result.HasValue)
            return Problem(result.Value);

        return NoContent();
    }
}
