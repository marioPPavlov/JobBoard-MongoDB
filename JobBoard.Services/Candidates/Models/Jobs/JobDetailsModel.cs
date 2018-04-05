using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using JobBoard.Services.Candidates.Models.Cvs;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobBoard.Services.Candidates.Models.Jobs
{
    public class JobDetailsModel : IMapFrom<Job>, IHaveCustomMapping
    {
        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Tags { get; set; } 

        public string MotivationalLetter { get; set; }

        public string AppliedCvId { get; set; }

        public IEnumerable<CvOverviewModel> Cvs { get; set; } = new List<CvOverviewModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobDetailsModel>();
        }
    }
}
