using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class PersonalInfoEditModel : IMapFrom<PersonalInfo>, IHaveCustomMapping
    {
    //    [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Name { get; set; }

    //    [Required]
        [EmailAddress]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Email { get; set; }

        //    [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Address { get; set; }

        [StringLength(PictureMax, MinimumLength = PictureMin)]
        public string Picture { get; set; }

     //   [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirhtDate { get; set; }

     //   [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    //    [Required]
        public string Gender { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<PersonalInfo, PersonalInfoEditModel>();
            mapper
              .CreateMap<PersonalInfoEditModel, PersonalInfo>();
        }
    }
}
