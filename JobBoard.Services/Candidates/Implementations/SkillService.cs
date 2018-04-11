using JobBoard.Common.Extensions;
using JobBoard.Data;
using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JobBoard.Services.Candidates.Implementations
{
    public class SkillService : GenericListCrudService<Skill, SkillModel, SkillListModel>, ISkillService
    {
        public SkillService(JobBoardDbContext db) : base(db)
        {
        }

        protected override List<Skill> GetCollectionById(string id)
        {
            var skills = this.db.Cvs.Find(c => c.Id == id.ToObjectId())
                 .SingleOrDefault()
                 .Skills
                 .ToList();

            return skills;
        }

        protected override string GetDbSetName()
        {
            return nameof(Cv.Skills);
        }
    }
}
