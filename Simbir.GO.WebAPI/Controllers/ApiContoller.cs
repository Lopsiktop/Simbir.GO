using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Simbir.GO.Application.Common.Interfaces.Authentication;
using Simbir.GO.Application.Common.Interfaces.UnitOfWork;

namespace Simbir.GO.WebAPI.Controllers;

[ApiController]
[Authorize]
public class ApiContoller : ControllerBase
{
    private readonly ICheckAccounts? _checkAccounts;

    public ApiContoller(ICheckAccounts? checkAccounts = null)
    {
        _checkAccounts = checkAccounts;
    }

    protected int GetUserId()
    {
        var sub = User.Claims.SingleOrDefault(x => x.Type.Contains("nameidentifier"))!.Value;
        return int.Parse(sub);
    }

    protected async Task<bool> TokenIsRevokedOrAccountDoesNotExist()
    {
        if(await TokenIsRevoked()) return true;
        if(!await AccountDoesExist()) return true;

        return false;
    }

    protected async Task<bool> AccountDoesExist()
    {
        if (_checkAccounts == null)
            return true;

        var id = GetUserId();

        var result = await _checkAccounts.AccountDoesExist(id);
        return result;
    }

    private string GetJwtToken() =>
        Request.Headers.Authorization.ToString().Split(' ').Last();

    protected async Task<bool> TokenIsRevoked()
    {
        if (_checkAccounts == null)
            return false;

        var token = GetJwtToken();
        var result = await _checkAccounts.TokenIsRevoked(token);

        if (result.IsError)
            return true;

        return result.Value;
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Failure => StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
            return Problem();

        if (errors.All(x => x.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        HttpContext.Items["errors"] = errors;
        return Problem(errors[0]);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        errors.ForEach(x => modelStateDictionary.AddModelError(x.Code, x.Description));
        return ValidationProblem(modelStateDictionary);
    }
}
