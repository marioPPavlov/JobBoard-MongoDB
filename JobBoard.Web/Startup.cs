using AutoMapper;
using JobBoard.Data;
using JobBoard.Web.Infrastructure.Extensions;
using JobBoard.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobBoard.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<JobBoardDbContext>();

            // Register identity framework services and also Mongo storage.   
            services.AddMongoIdentity(Configuration,
                    options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredUniqueChars = 0;
                        options.Password.RequiredLength = 3;
                    })
                    .AddDefaultTokenProviders(); 

            services.AddDomainServices();
            services.AddAutoMapper();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {  

                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCreatedRoles();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                //    routes.MapRoute(
                //            CandidateEdit,
                //            "{area}/{controller}/Edit/PersonalInfo/{id}",
                //             new { area = CandidateArea, controller = "Cvs", action = nameof(CvsController.PersonalInfo) });

                //routes.MapRoute(
                //            CandidateAdd,
                //            "{area}/{controller}/Add/{action}/{id}",
                //             new { area = CandidateArea, controller = "Cvs", action = nameof(CvsController.Work) });

                //routes.MapRoute(
                //    CandidateCreate,
                //    "{area}/{controller}/Create",
                //    new { area = CandidateArea, controller = nameof(CvsController), action = nameof(CvsController.Create) });

                routes.MapRoute(
                    "area-default",
                    "{area=Candidate}/{controller=Work}/{action=Index}/{id?}");

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
