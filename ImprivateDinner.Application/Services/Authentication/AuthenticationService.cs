using ImprivateDinner.Application.Common.Interfaces.Authentication;

namespace ImprivateDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        var guid = Guid.NewGuid();
        var token = jwtTokenGenerator.GenerateToken(guid, "firstName", "lastName");
        return new AuthenticationResult(guid, "firstName", "lastName", email, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        var guid = Guid.NewGuid();
        var token = jwtTokenGenerator.GenerateToken(guid, firstName, lastName);
        return new AuthenticationResult(guid, firstName, lastName, email, token);
    }
}