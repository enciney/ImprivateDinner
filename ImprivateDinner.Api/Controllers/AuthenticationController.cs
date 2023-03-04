using System.Net;
using ImprivateDinner.Api.Filters;
using ImprivateDinner.Application.Common.Errors;
using ImprivateDinner.Application.Services.Authentication;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ImprivateDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = authService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        if(result.IsSuccess)
        {
            return MapAuthResult(result.Value);
        }
        var firstError = result.Errors[0];
        if(firstError is DuplicateEmailError)
        {
            return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists");
        }
        return Problem();
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
        var authResult = authService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        return Ok(response);
    }
}