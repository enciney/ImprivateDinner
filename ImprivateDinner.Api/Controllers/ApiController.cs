using System.Net;
using ErrorOr;
using ImprivateDinner.Api.Common.Http;
using ImprivateDinner.Api.Filters;
using ImprivateDinner.Application.Common.Errors;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ImprivateDinner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    [Route("problem")]
    public IActionResult Problem(List<Error> errors)
    {
        if(errors.Count is 0)
        {
            return Problem();
        }
        if(errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();
            errors.ForEach(e => 
            {
                modelStateDictionary.AddModelError(e.Code, e.Description);
            });

            return ValidationProblem(modelStateDictionary);
        }
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