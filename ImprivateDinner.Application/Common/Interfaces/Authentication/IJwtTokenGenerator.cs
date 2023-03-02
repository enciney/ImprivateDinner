using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}