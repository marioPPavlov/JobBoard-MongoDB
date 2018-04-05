using AutoMapper;
using JobBoard.Common.DTO;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;
namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class EducationModel : IModel,IMapFrom<Education>, IHaveCustomMapping
    {
    //    [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Subject { get; set; }

     //   [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Qualification { get; set; }

 //       [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Organisation { get; set; }

        //[Required]
        public DateTime DateFrom { get; set; }
        //[Required]
        public DateTime DateTo { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<Education, EducationModel>();
            mapper
              .CreateMap<EducationModel, Education>();
        }
    }
}
