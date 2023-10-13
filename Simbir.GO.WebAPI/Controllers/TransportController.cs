using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Transports.Commands.CreateTransport;
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

    public TransportController(IMediator mediator, IMapper mapper)
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
        var command = _mapper.Map<CreateTransportCommand>((GetUserId(), request));
        var result = await _mediator.Send(command);

        return result.Match(
            transport => Ok(transport),
            errors => Problem(errors));
    }
}
