
using ImprivateDinner.Api;
using ImprivateDinner.Api.Common.Errors;
using ImprivateDinner.Api.Mapping;
using ImprivateDinner.Application;
using ImprivateDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
}

var app = builder.Build();
{   
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}