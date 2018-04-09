using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using MongoDB.Bson;
using System;


namespace JobBoard.Services.Employers.Models.Jobs
{
    public class JobModel : IMapFrom<Job>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string Location { get; set; }

        public string ShortDescription { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobModel>()
              .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Description.Substring(0, Math.Min(20, src.Description.Length))))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
