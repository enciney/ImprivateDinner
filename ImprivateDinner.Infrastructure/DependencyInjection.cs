using System.Text;
using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Common.Interfaces.Persistence;
using ImprivateDinner.Application.Common.Interfaces.Services;
using ImprivateDinner.Infrastructure.Authentication;
using ImprivateDinner.Infrastructure.Persistence;
using ImprivateDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ImprivateDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static void AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        // since we need jwtSetting to set our Token Bearer options, we are using below code blocks instead of .Configure
        // as a functionality they are same
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        // will automatically cover Authorization haeader to check token and if it is valid then mark HttContext.User.Identity.IsAutenticated as true
        services.AddAuthentication(defaultScheme : JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        });
    }
}