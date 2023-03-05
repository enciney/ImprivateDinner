using ErrorOr;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Interfaces.Persistence;
using ImprivateDinner.Application.Services.Authentication.Common;
using ImprivateDinner.Domain.Common.Errors;

namespace ImprivateDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user does exist
        var user = userRepository.GetUserByEmail(email);
        if (userRepository.GetUserByEmail(email) is null)
        {
            return Errors.User.Missing;
        }
        // 2. Validate the password is correct
        if(user?.Password != password)
        {
            return Errors.User.InvalidCredentials;
        }
        // 3. Create JWT Token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}