using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using MongoDB.Bson;
using System;


namespace JobBoard.Services.Candidates.Models.Jobs
{
    public class JobModel : IMapFrom<Job>, IHaveCustomMapping
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string Location { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobModel>();
             
        }
    }
}
