using Desafio.API;
using Desafio.Infrastructure;
using Desafio.Application;
using Serilog;

LoggerHelper.EnsureInitialized();
Log.Information("Inicializing Server...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Services.AddControllers();
        builder.Services.AddVersioning();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddApplicationConfigurations(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApiConfigurations();
    }

    var app = builder.Build();
    {
        app.UseExceptionMiddleware();
        app.AddBuilderConfiguration();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
catch (Exception e) when (!e.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    Log.Fatal(e, "unhandled exception");
    throw;
}
finally
{
    Log.Information("server shutting down..");
    Log.CloseAndFlush();
}

