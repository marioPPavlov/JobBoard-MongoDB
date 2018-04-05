using JobBoard.Data.Models;
using JobBoard.Data.Models.Cvs;
using JobBoard.Data.Models.Employers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace JobBoard.Data
{
    public class JobBoardDbContext
    {
        private IMongoDatabase database { get; }
        private IConfiguration configuration { get; set; }

        public JobBoardDbContext
            (
            IConfiguration configuration
            )
        {
            this.configuration = configuration;
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
                bool IsSSL = Convert.ToBoolean(configuration.GetSection("ConnectionStrings:IsSSL").Value);
                if(IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                this.database = mongoClient.GetDatabase(configuration.GetSection("ConnectionStrings:DatabaseName").Value);
            }
            catch(Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public IMongoCollection<Cv> Cvs
        {
            get
            {
                return this.database.GetCollection<Cv>("Cvs");
            }
        }

        public IMongoCollection<Job> Jobs
        {
            get
            {
                return this.database.GetCollection<Job>("Jobs");
            }
        }
    }
}
