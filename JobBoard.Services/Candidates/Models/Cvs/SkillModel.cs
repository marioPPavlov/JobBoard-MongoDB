using AutoMapper;
using JobBoard.Common.DTO;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System.ComponentModel.DataAnnotations;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class SkillModel : IModel, IMapFrom<Skill>, IHaveCustomMapping
    {

        //  [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Name { get; set; }

        //  [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Level { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Skill, SkillModel>();
            mapper
              .CreateMap<SkillModel, Skill>();
        }
    }
}
