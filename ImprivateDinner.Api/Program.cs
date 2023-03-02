
using ImprivateDinner.Api.Filters;
using ImprivateDinner.Api.Middleware;
using ImprivateDinner.Application;
using ImprivateDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers( options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers( );
}

var app = builder.Build();
{   
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}