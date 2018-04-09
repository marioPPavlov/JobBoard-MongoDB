using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using JobBoard.Services.Employers.Models.Cvs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Employers.Models.Jobs
{
    public class JobDetailsModel : IMapFrom<Job>, IHaveCustomMapping
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Tags { get; set; } 

        public List<CvOverviewModel> Cvs { get; set; } = new List<CvOverviewModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobDetailsModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
