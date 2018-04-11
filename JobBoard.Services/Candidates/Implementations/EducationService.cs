using JobBoard.Common.Extensions;
using JobBoard.Data;
using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JobBoard.Services.Candidates.Implementations
{
    class EducationService : GenericListCrudService<Education, EducationModel, EducationListModel>, IEducationService
    {
        public EducationService(JobBoardDbContext db) : base(db)
        {
        }

        protected override List<Education> GetCollectionById(string id)
        {
            var educations = this.db.Cvs.Find(c => c.Id == id.ToObjectId())
                 .SingleOrDefault()
                 .Educations
                 .ToList();

            return educations;
        }

        protected override string GetDbSetName()
        {
            return nameof(Cv.Educations);
        }
    }
}
