using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using ImprivateDinner.Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ImprivateDinner.Api.Common.Errors;

namespace ImprivateDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // builder.Services.AddControllers( options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        // AddAuthorization method is already called in AddControllers middleware so no need to call explicitly
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ImprivateDinnerProblemDetailsFactory>();
        services.AddMappings();
        return services;
        
    }

}