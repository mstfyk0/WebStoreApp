using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder applicationBuilder)
        {
            RepositoryContext context = applicationBuilder
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();

            }
        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                .AddSupportedUICultures("tr-TR")
                .SetDefaultCulture("tr-TR");

            }
             );
        }
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder applicationBuilder)
        {
            const string adminUser = "Admin";
            const string adminPassword = "admin+123456";

            //UserManager
            UserManager<IdentityUser> userManager = applicationBuilder
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
            ;

            //Role Manager
            RoleManager<IdentityRole> role = applicationBuilder
            .ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();


            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new IdentityUser()
                {
                    Email = "ygt99krk@gmail.com",
                    PhoneNumber = "55667788",
                    UserName = adminUser,
                };

                var reult = await userManager.CreateAsync(user, adminPassword);

                if (!reult.Succeeded)
                    throw new Exception("Admin user could not created.");

                var roleResult = await userManager.AddToRolesAsync(user, role
                            .Roles
                            .Select(r => r.Name)
                            .ToList()
                        );

                if (!roleResult.Succeeded)
                {
                    throw new Exception("System have problems with role defination for admin");
                }
            }
        }
    }
}