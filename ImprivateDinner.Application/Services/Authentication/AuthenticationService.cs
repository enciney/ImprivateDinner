using ErrorOr;
using FluentResults;
using ImprivateDinner.Application.Common.Errors;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Interfaces.Persistence;
using ImprivateDinner.Domain.Common.Errors;
using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
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