using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);