using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Common.Interfaces.Services;
using ImprivateDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ImprivateDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider dateTimeProvider;

    private readonly IOptions<JwtSettings> jwtOptions;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtOptions = jwtOptions;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.Value.Secret)),
                SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        };
        var securityToken = new JwtSecurityToken(
            claims: claims,
            audience: jwtOptions.Value.Audience,
            signingCredentials: signingCredentials,
            issuer: jwtOptions.Value.Audience,
            expires: dateTimeProvider.UtcNow.AddMinutes(jwtOptions.Value.ExpiryMinutes));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}