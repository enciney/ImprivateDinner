using ImprivateDinner.Application.Common.Errors;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Interfaces.Persistence;
using ImprivateDinner.Domain.Entities;
using OneOf;

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

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user does exist
        var user = userRepository.GetUserByEmail(email);
        if (userRepository.GetUserByEmail(email) is null)
        {
            throw new Exception($"user is not exist with '{email}' email address");
        }
        // 2. Validate the password is correct
        if(user?.Password != password)
        {
            throw new Exception($"Invalid credential for user that have '{email}' email address");
        }
        // 3. Create JWT Token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }

    public OneOf<AuthenticationResult,DuplicateEmailError> Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (userRepository.GetUserByEmail(email) is not null)
        {
           return new DuplicateEmailError();
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