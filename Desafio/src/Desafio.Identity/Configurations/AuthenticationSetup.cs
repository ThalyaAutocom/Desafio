﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Desafio.Identity;

public static class AuthenticationSetup
{
    public static void AddAuthenticationInformation(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtAppSettingsOptions = configuration.GetSection(nameof(JwtOptions));
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));

        //Configurar JwtOptions
        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtAppSettingsOptions[nameof(JwtOptions.Issuer)];
            options.Audience = jwtAppSettingsOptions[nameof(JwtOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512); //
            options.Expiration = int.Parse(jwtAppSettingsOptions[nameof(JwtOptions.Expiration)] ?? "0");
        });

        //Requisitos de Senha
        services.Configure<IdentityOptions>(options => //TODO: VERIFICAR ANOTAÇÃO EDUARDO
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        });

        //Dados do Token
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

            ValidateAudience = true,
            ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,

            RequireExpirationTime = true,
            ValidateLifetime = true,

            ClockSkew = TimeSpan.Zero
        };

        //Trabalhar com autenticação
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => //Autenticação vinda do Jwt
        {
            options.TokenValidationParameters = tokenValidationParameters;
        });


    }
}
