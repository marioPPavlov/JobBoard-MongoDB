using AutoMapper;
using JobBoard.Common.DTO;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class WorkModel : IModel,IMapFrom<Work>, IHaveCustomMapping
    {
        //[Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Position { get; set; }

        //[Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Employer { get; set; }

        //[Required]
        public DateTime DateFrom { get; set; }
        //[Required]
        public DateTime DateTo { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<Work, WorkModel>();
            mapper
              .CreateMap<WorkModel, Work>();
        }
    }
}
