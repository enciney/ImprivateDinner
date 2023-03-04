using ErrorOr;
using FluentResults;
using ImprivateDinner.Application.Common.Errors;

namespace ImprivateDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    ErrorOr<AuthenticationResult> Login(string email, string password);
}