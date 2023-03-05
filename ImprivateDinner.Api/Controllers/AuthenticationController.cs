using ImprivateDinner.Application.Services.Authentication;
using ImprivateDinner.Domain.Common.Errors;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ImprivateDinner.Application.Services.Authentication.Commands;
using ImprivateDinner.Application.Services.Authentication.Queries;
using ImprivateDinner.Application.Services.Authentication.Common;

namespace ImprivateDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationQueryService authQueryService;
    private readonly IAuthenticationCommandService authCommandService;

    public AuthenticationController(IAuthenticationCommandService authCommandService, IAuthenticationQueryService authQueryService)
    {
        this.authCommandService = authCommandService;
        this.authQueryService = authQueryService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = authCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        return result.Match(
            authresult => Ok(MapAuthResult(authresult)),
            errors => Problem(errors)
        );
    }

    private IActionResult MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = authQueryService.Login(request.Email, request.Password);

        if(result.IsError && result.FirstError == Errors.User.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
            title: result.FirstError.Description);
        }
        return result.Match(
            authresult => Ok(MapAuthResult(authresult)),
            errors => Problem(errors)
        );
    }
}