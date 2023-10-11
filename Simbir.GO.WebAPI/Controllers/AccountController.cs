﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Accounts.Commands.Register;
using Simbir.GO.Application.Accounts.Commands.Update;
using Simbir.GO.Application.Accounts.Queries.GetMe;
using Simbir.GO.Application.Accounts.Queries.SingIn;
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

    private int GetUserId()
    {
        var sub = User.Claims.SingleOrDefault(x => x.Type.Contains("nameidentifier"))!.Value;
        return int.Parse(sub);
    }

    [HttpGet("Me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        int userId = GetUserId();
        var query = new GetMeQuery(userId);
        var result = await _mediator.Send(query);

        return result.Match(
            account => Ok(_mapper.Map<AccountResponse>(account)),
            errors => Problem(errors));
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

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(AccountRequest request)
    {
        var query = _mapper.Map<SignInAccountQuery>(request);
        var result = await _mediator.Send(query);

        return result.Match(
            account => Ok(_mapper.Map<AccountTokenResponse>(account)),
            errors => Problem(errors));
    }

    [HttpPut("Update")]
    [Authorize]
    public async Task<IActionResult> Update(AccountRequest request)
    {
        int userId = GetUserId();
        var command = new UpdateAccountCommand(userId, request.Username, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(
            account => Ok(_mapper.Map<AccountResponse>(account)),
            errors => Problem(errors));
    }
}
