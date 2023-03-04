
using ImprivateDinner.Api.Errors;
using ImprivateDinner.Api.Filters;
using ImprivateDinner.Api.Middleware;
using ImprivateDinner.Application;
using ImprivateDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers( options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers( );
    builder.Services.AddSingleton<ProblemDetailsFactory, ImprivateDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{   
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}