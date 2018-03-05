using global::MongoDB.Driver;
using JobBoard.Data.Models;
using JobBoard.Data.Models.MongoDB.Identity;
using JobBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace JobBoard.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions 
    {
        public static IdentityBuilder RegisterMongoStores<TUser, TRole>(this IdentityBuilder builder, IConfiguration configuration)
			where TRole : MongoRole
			where TUser : User
		{
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var url = new MongoUrl(connectionString);
			var client = new MongoClient(url);
            var database = client.GetDatabase(configuration.GetSection("ConnectionStrings:DatabaseName").Value);

            if(database == null)
            {
                throw new ArgumentException("Your connection string must contain a database name", connectionString);
            }
            return builder.RegisterMongoStores(
				p => database.GetCollection<TUser>("users"),
				p => database.GetCollection<TRole>("roles"));
		}

		public static IdentityBuilder RegisterMongoStores<TUser, TRole>(this IdentityBuilder builder,
			Func<IServiceProvider, IMongoCollection<TUser>> usersCollectionFactory,
			Func<IServiceProvider, IMongoCollection<TRole>> rolesCollectionFactory)
			where TRole : MongoRole
			where TUser : User
		{
			if (typeof(TUser) != builder.UserType)
			{
				var message = "User type passed to RegisterMongoStores must match user type passed to AddIdentity. "
				              + $"You passed {builder.UserType} to AddIdentity and {typeof(TUser)} to RegisterMongoStores, "
				              + "these do not match.";
				throw new ArgumentException(message);
			}
			if (typeof(TRole) != builder.RoleType)
			{
				var message = "Role type passed to RegisterMongoStores must match role type passed to AddIdentity. "
				              + $"You passed {builder.RoleType} to AddIdentity and {typeof(TRole)} to RegisterMongoStores, "
				              + "these do not match.";
				throw new ArgumentException(message);
			}
			builder.Services.AddSingleton<IUserStore<TUser>>(p => new UserStore<TUser>(usersCollectionFactory(p)));
			builder.Services.AddSingleton<IRoleStore<TRole>>(p => new RoleStore<TRole>(rolesCollectionFactory(p)));

			return builder;
		}

        public static IdentityBuilder AddMongoIdentity(this IServiceCollection services, IConfiguration configuration, Action<IdentityOptions> setupAction)
        {
            return services.AddMongoIdentityUsingCustomTypes<User, MongoRole>(configuration, setupAction);
        }

        public static IdentityBuilder AddMongoIdentityUsingCustomTypes<TUser, TRole>(this IServiceCollection services,IConfiguration configuration, Action<IdentityOptions> options)
          where TUser : User
          where TRole : MongoRole
        {
            return services.AddIdentity<TUser, TRole>(options)    
                .RegisterMongoStores<TUser, TRole>(configuration);
        }

        public static IServiceCollection AddDomainServices(
           this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Implementation));

            return services;
        }
    }
}