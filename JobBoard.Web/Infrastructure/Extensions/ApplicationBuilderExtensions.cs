using JobBoard.Data;
using JobBoard.Data.Models;
using JobBoard.Data.Models.Employers;
using JobBoard.Data.Models.MongoDB.Identity;
using JobBoard.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Infrastructure.Extensions
{


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddRolesAndJobs(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<JobBoard.Data.Models.User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<MongoRole>>();
                var db = serviceScope.ServiceProvider.GetService<JobBoardDbContext>();

                Task
                    .Run(async () =>
                    {
                        var roles = new[]
                        {
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

                        string EmployerEmail = "employer@employer.com";
                        if (userManager.FindByEmailAsync(EmployerEmail).Result == null)
                        {

                            var user = new User
                            {
                                UserName = EmployerEmail,
                                Email = EmployerEmail
                            };
                            var result = await userManager.CreateAsync(user, "employer");
                            await userManager.AddToRoleAsync(user, EmployerRole);
                        }

                        if (db.Jobs.Count( c => true) == 0)
                        {
                            var userId = userManager.FindByEmailAsync(EmployerEmail).Result.Id;
                            string jobDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent consectetur tempus nibh, condimentum mattis neque tincidunt vitae. Mauris consectetur velit pellentesque arcu aliquet iaculis. Nam ac felis in nisi ornare vestibulum. In dolor libero, sollicitudin sit amet feugiat in, vestibulum vitae libero. Nulla at est odio. Curabitur euismod dapibus sollicitudin. Aenean a fermentum ex. Suspendisse potenti.";
                            IEnumerable<Job> jobs = new List<Job>
                            {
                                new Job{ Title = "C# Programmer", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdGg3mAUTr7qLHONFEeuoEeaKFOqPh24q5LWHTP0orXcETFIWm",Location = "Sofia",Tags = ".Net C#"},
                                new Job{ Title = "Java Programmer", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://thumbs.dreamstime.com/z/job-search-22234226.jpg",Location = "Sofia",Tags = "Java js"},
                                new Job{ Title = "ConstructionWorker", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://tse1.mm.bing.net/th?id=OIP.N4XeN6Nm-3XZciO7jlRn5AHaHa&pid=Api",Location = "Ruse",Tags = "Hard Worker"},
                                new Job{ Title = "HR Manager", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://tse1.mm.bing.net/th?id=OIP.xbhR_tamFUOZhoWoU91TyAHaEo&pid=Api",Location = "UK",Tags = "HR"},
                                new Job{ Title = "Firefighter", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSvOlDerb9DwT0umTD2-rNsrvxx1Kk7GChwaNfAq1CDZu7EtsvYKw",Location = "UK",Tags = "HR"},

                                new Job{ Title = "Another C# Programmer", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://image.flaticon.com/icons/svg/52/52782.svg",Location = "Sofia",Tags = ".Net C#"},
                                new Job{ Title = "Another Java Programmer", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://thumbs.dreamstime.com/z/job-search-22234226.jpg",Location = "Sofia",Tags = "Java js"},
                                new Job{ Title = "Another ConstructionWorker", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://tse1.mm.bing.net/th?id=OIP.N4XeN6Nm-3XZciO7jlRn5AHaHa&pid=Api",Location = "Ruse",Tags = "Hard Worker"},
                                new Job{ Title = "Anotether HR Manager", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://tse1.mm.bing.net/th?id=OIP.xbhR_tamFUOZhoWoU91TyAHaEo&pid=Api",Location = "UK",Tags = "HR"},
                                new Job{ Title = "Another Firefighter", UserId = userId.ToObjectId(), Description = jobDescription,Picture="https://blog.udemy.com/wp-content/uploads/2014/05/bigstock-Firefighter-holding-mask-and-a-38631415-300x450.jpg",Location = "UK",Tags = "HR"},
                            };
                           
                            db.Jobs.InsertMany(jobs);
                        }
                    })
                    .Wait();

            }
            return app;
        }
    }
}
