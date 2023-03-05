using ImprivateDinner.Application.Services.Authentication;
using ImprivateDinner.Application.Services.Authentication.Commands;
using ImprivateDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ImprivateDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandService,AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService,AuthenticationQueryService>();
        return services;
        
    }

}