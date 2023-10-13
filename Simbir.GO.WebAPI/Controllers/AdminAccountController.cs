using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminAccounts.Commands.CreateAccount;
using Simbir.GO.Application.AdminAccounts.Common;
using Simbir.GO.Application.AdminAccounts.Queries;
using Simbir.GO.Application.AdminAccounts.Queries.GetAccount;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Contracts.AccountContracts;
using Simbir.GO.Infrastructure.Identity;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Admin/Account")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class AdminAccountController : ApiContoller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminAccountController(ICheckToken checkToken, IMediator mediator, IMapper mapper) : base(checkToken)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts(int start, int count)
    {
        if (await TokenIsRevoked()) return Unauthorized();

        var query = new GetAllAccountsAdminQuery(start, count);
        var result = await _mediator.Send(query);

        return result.Match(
            accounts => Ok(accounts),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        if (await TokenIsRevoked()) return Unauthorized();

        var query = new GetAccountAdminQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            account => Ok(account),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(AdminAccountRequest request)
    {
        if (await TokenIsRevoked()) return Unauthorized();

        var command = _mapper.Map<CreateAccountAdminCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            account => Ok(account),
            errors => Problem(errors));
    }
}
