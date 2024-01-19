﻿using Desafio.Application;
using Desafio.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Desafio.Infrastructure;

internal static class AddServicesStartup
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
