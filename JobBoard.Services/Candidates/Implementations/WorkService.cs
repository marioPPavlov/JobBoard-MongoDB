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
    class WorkService : GenericListCrudService<Work, WorkModel, WorkListModel>, IWorkService
    {
        public WorkService(JobBoardDbContext db) : base(db)
        {
        }

        protected override List<Work> GetCollectionById(string id)
        {
            var works = this.db.Cvs.Find(c => c.Id == id.ToObjectId())
                 .SingleOrDefault()
                 .Works
                 .ToList();

            return works;
        }

        protected override string GetDbSetName()
        {
            return nameof(Cv.Works);
        }
    }
}
