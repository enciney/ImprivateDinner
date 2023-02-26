using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace ImprivateDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        this.dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("super-secret-key")),
                SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
        };
        var securityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            issuer: "ImprivateDinner",
            expires: dateTimeProvider.UtcNow.AddMinutes(60));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}