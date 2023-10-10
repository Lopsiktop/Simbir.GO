using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Accounts.Commands.Register;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Contracts.AccountContracts;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Infrastructure.Persistence;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Account")]
public class AccountController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(AccountRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            account => Ok(_mapper.Map<AccountResponse>(account)),
            errors => Problem(errors));
    } 
}
