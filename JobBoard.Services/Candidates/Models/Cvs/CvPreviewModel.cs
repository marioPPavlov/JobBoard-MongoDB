using AutoMapper;
using JobBoard.Common.Mapping;
using JobBoard.Data.Models.Cvs;
using System.Collections.Generic;

namespace JobBoard.Services.Candidates.Models.Cvs
{
    public class CvPreviewModel : IMapFrom<Cv>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public PersonalInfo PersonalInfo { get; set; }

        public List<WorkModel> Works { get; set; } = new List<WorkModel>();

        public List<EducationModel> Educations { get; set; } = new List<EducationModel>();

        public List<LanguageModel> Languages { get; set; } = new List<LanguageModel>();

        public List<SkillModel> Skills { get; set; } = new List<SkillModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
               .CreateMap<Cv, CvPreviewModel>();
            mapper
                .CreateMap<CvPreviewModel, Cv>();
        }
    }
}
