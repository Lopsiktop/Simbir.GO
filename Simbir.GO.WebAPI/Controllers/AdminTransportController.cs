using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminTransports.Queries.GetAllTransport;
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
        var query = new GetAllTransportQuery(start, count, transportType);
        var result = await _mediator.Send(query);

        return result.Match(
            transports => Ok(transports),
            errors => Problem(errors));
    }
}
