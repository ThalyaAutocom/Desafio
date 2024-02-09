using Desafio.Application;
using System.Reflection;

namespace Desafio.API;

internal static class CorsConfiguration
{
    public static IServiceCollection AddCorsAPI(this IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(["http://localhost:7141/"]);
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
                policy.SetIsOriginAllowed(_ => true);
            });
        });

        return services;
    }
}


