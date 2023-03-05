using ErrorOr;
using ImprivateDinner.Application.Authentication.Common;
using MediatR;

namespace ImprivateDinner.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;