using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class CvOverviewModel : IMapFrom<Cv>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }



        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Cv, CvOverviewModel>()
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.PersonalInfo.Picture));
        }
    }
}
