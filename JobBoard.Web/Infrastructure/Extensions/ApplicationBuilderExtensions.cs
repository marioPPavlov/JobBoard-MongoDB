using JobBoard.Data;
using JobBoard.Data.Models.MongoDB.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Infrastructure.Extensions
{


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCreatedRoles(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<JobBoard.Data.Models.User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<MongoRole>>();
                var db = serviceScope.ServiceProvider.GetService<JobBoardDbContext>();

                Task
                    .Run(async () =>
                    {

                        var adminName = AdministratorRole;

                        var roles = new[]
                        {
                                adminName,
                                CandidateRole,
                                EmployerRole
                        };

                        foreach(var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if(!roleExists)
                            {
                                await roleManager.CreateAsync(new MongoRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "admin@mysite.com";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if(adminUser == null)
                        {
                            adminUser = new JobBoard.Data.Models.User
                            {
                                Email = adminEmail,
                                UserName = adminName,
                                Name = adminName,
                            };

                            await userManager.CreateAsync(adminUser, "admin123");

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    })
                    .Wait();

            }

            return app;
        }
    }
}
