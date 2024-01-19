using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Application;

internal static class AddExceptionMiddlewareStartup
{
    internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
        return services;
    }
}
