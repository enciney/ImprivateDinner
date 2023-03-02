using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);