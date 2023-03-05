using ErrorOr;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Interfaces.Persistence;
using ImprivateDinner.Application.Services.Authentication.Common;
using ImprivateDinner.Domain.Common.Errors;
using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Services.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }
    
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        // 2. Create User(generate unique ID) & Persist to DB        
        var user = new User(){
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        userRepository.Add(user);
        // 3. Create JWT Token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}