using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Employers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Employers.Models.Jobs
{
    public class JobDetailsModel : IMapFrom<Job>, IHaveCustomMapping
    {
        
        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Tags { get; set; } 

        public string MotivationalLetter { get; set; }

        //public List<CvListingModel> Cvs { get; set; } = new List<CvListingModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobDetailsModel>();
        }
    }
}
