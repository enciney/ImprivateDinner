using ImprivateDinner.Api.Filters;
using ImprivateDinner.Application.Services.Authentication;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ImprivateDinner.Api.Controllers;

[ApiController]
[Route("auth")]
[ErrorHandlingFilter]
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
        var authResult = authService.Register(request.FirstName, request.LastName, request.Email, request.Password);
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