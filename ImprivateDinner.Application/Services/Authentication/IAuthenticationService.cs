using FluentResults;
using ImprivateDinner.Application.Common.Errors;

namespace ImprivateDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    AuthenticationResult Login(string email, string password);
}