using AutoMapper;
using JobBoard.Data;
using JobBoard.Data.Models;
using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace JobBoard.Services.Candidates.Implementations
{
    public class CvService : ICvService
    {
        private readonly JobBoardDbContext db;
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> userManager;

        public CvService(
            JobBoardDbContext db,
            IHttpContextAccessor context,
            UserManager<User> userManager
        )
        {
            this.db = db;
            _context = context;
            this.userManager = userManager;
        }

        public string GetLoggedUser()
        {
            return  userManager.GetUserId(_context.HttpContext.User);
        }

        public string Add(CvCreateModel model)
        {
            var cv = Mapper.Map<Cv>(model);
            cv.UserId =  ObjectId.Parse(GetLoggedUser());
            cv.Id = ObjectId.GenerateNewId();

            this.db.Cvs.InsertOneAsync(cv);
            return cv.Id.ToString();
        }

        public string GetUserIdByCvId(string id)
            => this.db.Cvs.Find(c => c.Id == ObjectId.Parse(id)).SingleOrDefault().UserId.ToString();

        public PersonalInfoEditModel GetPersonalInfoById(string id)
        {
            var personalInfo =  this.db.Cvs.Find(c => c.Id == ObjectId.Parse(id)).SingleOrDefault().PersonalInfo;
            var personalInfoEditModel = Mapper.Map<PersonalInfoEditModel>(personalInfo);

            return personalInfoEditModel;
        }

        public void UpdatePersonalInfo(string id, PersonalInfoEditModel form)
        {
            var personalInfo = Mapper.Map<PersonalInfo>(form);

            var updateResult = this.db.Cvs.UpdateOne(
                    Builders<Cv>.Filter.Eq("_id", ObjectId.Parse(id)),
                    Builders<Cv>.Update.Set(nameof(Cv.PersonalInfo), personalInfo));
        }

        public bool CvBelongsToLoggedUser(string cvId)
        {
            string cvUserId = GetUserIdByCvId(cvId);
            string userId = GetLoggedUser();

            if(cvUserId == userId)
            {
                return true;
            }
            return false;
        }

    }
}
