using Desafio.Domain;
using Desafio.Identity;
using Desafio.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API;

internal static class DbMigrationHelperExtension
{
    internal static IApplicationBuilder UseDbMigrationHelper(this IApplicationBuilder app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
        return app;
    }
}
internal static class DbMigrationHelpers
{
    internal static async Task EnsureSeedData(IApplicationBuilder serviceScope)
    {
        var services = serviceScope.ApplicationServices.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    internal static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        if (env.IsDevelopment())
        {
            //Utilizar com Postgres e SQLite
            await identityContext.Database.MigrateAsync();
            await appDbContext.Database.MigrateAsync();

            //Utilizar com InMemory
            //identityContext.Database.EnsureCreated();
            //appDbContext.Database.EnsureCreated();

            //Usar caso for necessário criar dados iniciais
            await EnsureSeedUserLevel(identityContext, userManager);
        }
    }

    internal static async Task EnsureSeedUserLevel(IdentityContext identityContext, UserManager<User> userManager)
    {
        #region Roles
        EUserLevel[] roles = (EUserLevel[])Enum.GetValues(typeof(EUserLevel));
        foreach(var role in roles)
        {
            if(!identityContext.Roles.Any(x => x.Name == role.ToString().ToUpper()))
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role.ToString().ToUpper(),
                    NormalizedName = role.ToString().ToUpper()
                };
                await identityContext.Roles.AddAsync(identityRole);
            }

        }
        #endregion

        #region User
        if (!userManager.Users.Any())
        {
            User user = new()
            {
                Name = "ADMINISTRATOR",
                NickName = "ADMINISTRATOR",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                Enable = true,
                UserLevel = EUserLevel.Administrator
            };

            var result = await userManager.CreateAsync(user, "Admin2024@");
            if (!result.Succeeded) throw new Exception("Não foi possível cadastrar um usuário padrão");

            //desbloquear usuário já que não terá e-mail de confirmação
            await userManager.SetLockoutEnabledAsync(user, false);

            var resultRole = await userManager.AddToRoleAsync(user, "ADMINISTRATOR");
            if (!result.Succeeded) throw new Exception("Não foi possível vincular uma permissão ao usuário padrão cadastrado");
        }
        #endregion

        await identityContext.SaveChangesAsync();
    }
}
