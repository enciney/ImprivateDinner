using ErrorOr;
using ImprivateDinner.Application.Authentication.Common;
using MediatR;

namespace ImprivateDinner.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;