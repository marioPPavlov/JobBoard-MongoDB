using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;
namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class CvCreateModel : IMapFrom<Cv>, IHaveCustomMapping
    {
        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Title { get; set; }

        [StringLength(PictureMax, MinimumLength = PictureMin)]
        public string Picture { get; set; }

//        public PersonalInfoCreateModel PersonalInfo { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Cv, CvCreateModel>();

            mapper
                .CreateMap<CvCreateModel, Cv>();
        }
    }
}
