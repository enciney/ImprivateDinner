using System.Net;
using ErrorOr;
using ImprivateDinner.Api.Common.Http;
using ImprivateDinner.Api.Filters;
using ImprivateDinner.Application.Common.Errors;
using ImprivateDinner.Application.Services.Authentication;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ImprivateDinner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    [Route("problem")]
    public IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items.Add(HttpContextItemKeys.Errors, errors);
        var firstError = errors[0];
        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
        return Problem(title: firstError.Description, statusCode: statusCode);
    }
}