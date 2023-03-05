using ErrorOr;
using ImprivateDinner.Application.Services.Authentication.Common;

namespace ImprivateDinner.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}