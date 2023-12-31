﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.AdminAccounts.Commands.CreateAccount;
using Simbir.GO.Application.AdminAccounts.Commands.DeleteAccount;
using Simbir.GO.Application.AdminAccounts.Commands.UpdateAccount;
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

    public AdminAccountController(ICheckAccounts checkAccounts, IMediator mediator, IMapper mapper) : base(checkAccounts)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts(int start, int count)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetAllAccountsAdminQuery(start, count);
        var result = await _mediator.Send(query);

        return result.Match(
            accounts => Ok(accounts),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var query = new GetAccountAdminQuery(id);
        var result = await _mediator.Send(query);

        return result.Match(
            account => Ok(account),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(AdminAccountRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = _mapper.Map<CreateAccountAdminCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            account => Ok(account),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CreateAccount(int id, AdminAccountRequest request)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new UpdateAccountAdminCommand(id, request.Username, request.Password, request.IsAdmin, request.Balance); ;
        var result = await _mediator.Send(command);

        return result.Match(
            account => Ok(account),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        if (await TokenIsRevokedOrAccountDoesNotExist()) return Unauthorized();

        var command = new DeleteAccountAdminCommand(id);
        var result = await _mediator.Send(command);

        if (result.HasValue)
            return Problem(result.Value);

        return NoContent();
    }
}
