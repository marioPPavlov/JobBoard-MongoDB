using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Employers.Models.Cvs
{

    public class CvOverviewModel : IMapFrom<Cv>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Picture { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Cv, CvOverviewModel>()
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.PersonalInfo.Picture))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PersonalInfo.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PersonalInfo.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.PersonalInfo.Address));
        }
    }

}
