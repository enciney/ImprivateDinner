using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using ImprivateDinner.Application.Common.Behaviors;
using ErrorOr;
using ImprivateDinner.Application.Authentication.Common;
using FluentValidation;
using ImprivateDinner.Application.Authentication.Commands.Register;

namespace ImprivateDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        // this is the registeration of mediatR middleware before and/or after the handler as a generic way
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // register all IValidators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
        
    }

}