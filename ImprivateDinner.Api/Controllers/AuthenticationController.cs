using ImprivateDinner.Domain.Common.Errors;
using ImprivateDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ImprivateDinner.Application.Authentication.Commands.Register;
using ImprivateDinner.Application.Authentication.Queries.Login;
using ImprivateDinner.Application.Authentication.Common;
using MapsterMapper;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;

namespace ImprivateDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    // it means no need authorization
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);
        var result = await mediator.Send(command);
        return GetResult(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);
        var result = await mediator.Send(query);
        if (result.IsError && result.FirstError == Errors.User.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
            title: result.FirstError.Description);
        }
        return GetResult(result);
    }

    private IActionResult GetResult(ErrorOr<AuthenticationResult> result)
    {
        return result.Match(
            authresult => Ok(mapper.Map<AuthenticationResponse>(authresult)),
            errors => Problem(errors)
        );
    }
}