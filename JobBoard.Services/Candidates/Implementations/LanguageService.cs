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
    class LanguageService : GenericListCrudService<Language, LanguageModel, LanguageListModel>, ILanguageService
    {
        public LanguageService(JobBoardDbContext db) : base(db)
        {
        }

        protected override List<Language> GetCollectionById(string id)
        {
            var languages = this.db.Cvs.Find(c => c.Id == id.ToObjectId())
                     .SingleOrDefault()
                     .Languages
                     .ToList();

            return languages;
        }

        protected override string GetDbSetName()
        {
            return nameof(Cv.Languages);
        }
    }
}
