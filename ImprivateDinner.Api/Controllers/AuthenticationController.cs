using ImprivateDinner.Domain.Common.Errors;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ImprivateDinner.Application.Authentication.Commands.Register;
using ImprivateDinner.Application.Authentication.Queries.Login;
using ImprivateDinner.Application.Authentication.Common;

namespace ImprivateDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;

    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var result = await mediator.Send(command);
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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await mediator.Send(query);
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