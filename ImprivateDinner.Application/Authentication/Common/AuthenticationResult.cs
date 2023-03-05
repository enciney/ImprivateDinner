using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Authentication.Common;
public record AuthenticationResult(
    User User,
    string Token
);