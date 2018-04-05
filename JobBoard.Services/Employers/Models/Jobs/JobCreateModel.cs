using JobBoard.Common.Mapping;

using static JobBoard.Data.Models.Constants.Constraints;
using JobBoard.Data.Models.Employers;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace JobBoard.Services.Employers.Models.Jobs
{
    public class JobCreateModel : IMapFrom<Job>, IHaveCustomMapping
    {
        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Title { get; set; }

        [StringLength(PictureMax, MinimumLength = PictureMin)]
        public string Picture { get; set; }

        [Required]
        [StringLength(DescriptionMax, MinimumLength = DescriptionMin)]
        public string Description { get; set; }

        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Location { get; set; }

        [StringLength(TagMax, MinimumLength = TagMin)]
        public string Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
              .CreateMap<Job, JobCreateModel>();
            mapper
              .CreateMap<JobCreateModel, Job>();
        }
    }
}
