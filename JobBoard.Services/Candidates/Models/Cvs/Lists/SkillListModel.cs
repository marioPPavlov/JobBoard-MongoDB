using JobBoard.Common.DTO;
using System.Collections.Generic;

namespace JobBoard.Services.Candidates.Models.Cvs.Lists
{
    public class SkillListModel : IListModel<SkillModel>
    {
        public List<SkillModel> Skills { get; set; } = new List<SkillModel>();

        public List<SkillModel> GetList()
        {
            return this.Skills;
        }
    }
}
