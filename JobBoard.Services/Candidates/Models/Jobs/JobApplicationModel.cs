using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Candidates.Models.Jobs
{
    public class JobApplicationModel : IMapFrom<JobApplication>, IHaveCustomMapping
    {
        public string MotivationalLetter { get; set; }

        public string AppliedCvId { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.
                CreateMap<JobApplication, JobApplicationModel>();

            mapper.
                 CreateMap<JobApplicationModel, JobApplication>()
                 .ForMember(dest => dest.AppliedCvId, opt => opt.MapFrom(src => ObjectId.Parse(src.AppliedCvId)));
        }
    }
}
