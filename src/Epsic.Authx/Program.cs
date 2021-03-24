using System.Data;
using System;
using Epsic.Authx.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Epsic.Authx
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CovidDbContext>();
                    await context.Database.MigrateAsync();

                    var usersManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await rolesManager.RoleExistsAsync("admin"))
                    {
                        var user = new IdentityUser
                        {
                            UserName = "Selmir",
                            Email = "selmir@epsic.ch",
                            EmailConfirmed = true
                        };
                        await usersManager.CreateAsync(user, "Ep$icCovid2021");
                        var adminRole = await rolesManager.CreateAsync(new IdentityRole("admin"));
                        var userRole = await rolesManager.CreateAsync(new IdentityRole("user"));
                        await usersManager.AddClaimAsync(user, new Claim("IsMedecin", "false"));

                        await usersManager.AddToRoleAsync(user, "admin");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}