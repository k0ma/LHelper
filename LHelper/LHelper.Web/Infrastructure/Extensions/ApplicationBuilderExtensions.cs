namespace LHelper.Web.Infrastructure.Extensions
{
    using Data;
    using LHelper.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                serviceScope.ServiceProvider
                    .GetService<LHelperDbContext>()
                    .Database
                    .Migrate();

                var roleManager = serviceScope.ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScope.ServiceProvider
                    .GetService<UserManager<User>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.AdministratorRole;

                        var roles = new[]
                        {
                            adminName,
                            WebConstants.TrainerRole
                        };

                        foreach (var role in roles)
                        {


                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }

                        }

                        var adminEmail = "p.kem@abv.bg";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminName,
                                Name = adminName
                            };

                            await userManager.CreateAsync(adminUser, "admin12");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                    .GetAwaiter()
                    .GetResult();

            }

            return app;
        }
    }
}
