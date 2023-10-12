using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminAccounts.Queries;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Infrastructure.Identity;

namespace Simbir.GO.WebAPI.Controllers;

[Route("api/Admin/Account")]
[Authorize(Policy = IdentityPolicy.AdminPolicy)]
public class AdminAccountController : ApiContoller
{
    private readonly IMediator _mediator;

    public AdminAccountController(ICheckToken checkToken, IMediator mediator) : base(checkToken)
    {
        _mediator = mediator;
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
}
