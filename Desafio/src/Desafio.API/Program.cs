using Desafio.API;
using Desafio.Infrastructure;
using Desafio.Identity;
using Desafio.Application;
using AutoMapper;
using Desafio.API.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddVersioning();
    builder.Services.AddSwagger();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddIdentityConfiguration(builder.Configuration);
    builder.Services.AddApplicationConfigurations(builder.Configuration);
    //builder.Services.AddAutoMapperConfiguration();
    builder.Services.AddAutoMapper(typeof(Program));
}

var app = builder.Build();
{
    var mapconfig = new MapperConfiguration(config => config.AddProfile<AutoMapperConfig>());
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseDbMigrationHelper();

    app.Run();
}
