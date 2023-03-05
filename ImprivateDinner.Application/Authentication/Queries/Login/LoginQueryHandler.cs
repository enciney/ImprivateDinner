using System.Net;
using ErrorOr;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Common.Interfaces.Persistence;
using ImprivateDinner.Domain.Entities;
using ImprivateDinner.Domain.Common.Errors;
using MediatR;
using ImprivateDinner.Application.Authentication.Common;

namespace ImprivateDinner.Application.Authentication.Queries.Login;

//Handle LoginQuery and response ErrorOr<AuthenticationResult>
public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // to get rid of the warning that coming by compiler about async
        await Task.CompletedTask;
        // 1. Validate the user does exist
        var user = userRepository.GetUserByEmail(query.Email);
        if (userRepository.GetUserByEmail(query.Email) is null)
        {
            return Errors.User.Missing;
        }
        // 2. Validate the password is correct
        if(user?.Password != query.Password)
        {
            return Errors.User.InvalidCredentials;
        }
        // 3. Create JWT Token
        var token = jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}