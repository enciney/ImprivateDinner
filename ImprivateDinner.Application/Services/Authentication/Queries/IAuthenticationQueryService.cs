using ErrorOr;
using ImprivateDinner.Application.Services.Authentication.Common;

namespace ImprivateDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}