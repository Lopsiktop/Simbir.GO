using Microsoft.AspNetCore.Mvc;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Contracts.AccountContracts;
using Simbir.GO.Domain.AccountEntity;
using Simbir.GO.Infrastructure.Persistence;

namespace Simbir.GO.WebAPI.Controllers;

[ApiController]
[Route("api/Account")]
public class AccountController : ControllerBase
{
    private readonly SimbirDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AccountController(SimbirDbContext context, IJwtTokenGenerator jwtTokenGenerator)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("SignIn")]
    public IActionResult SignIn(AccountRequest request)
    {
        var account = Account.Create(request.Username, request.Password);
        _context.Accounts.Add(account);
        _context.SaveChanges();

        var token = _jwtTokenGenerator.GenerateToken(account.Id, account.IsAdmin);

        return Ok(new
        {
            account,
            token
        });
    } 
}
