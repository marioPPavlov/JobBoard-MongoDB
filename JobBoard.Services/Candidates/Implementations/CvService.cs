using AutoMapper;
using JobBoard.Data;
using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace JobBoard.Services.Candidates.Implementations
{
    public class CvService : ICvService
    {
        private readonly JobBoardDbContext db;


        public CvService(
            JobBoardDbContext db
            )
        {
            this.db = db;
        }

        public async Task Add(CvCreateModel model, string userId)
        {
            var cv = Mapper.Map<Cv>(model);
            cv.UserId =  ObjectId.Parse(userId);

            await this.db.Cvs.InsertOneAsync(cv);
        }
    }
}
