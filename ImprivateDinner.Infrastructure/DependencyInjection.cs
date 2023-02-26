using ImprivateDinner.Application.Common.Interfaces.Authentication;
using ImprivateDinner.Application.Common.Interfaces.Services;
using ImprivateDinner.Infrastructure.Authentication;
using ImprivateDinner.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImprivateDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator,JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider,DateTimeProvider>();
        return services;
    }
}