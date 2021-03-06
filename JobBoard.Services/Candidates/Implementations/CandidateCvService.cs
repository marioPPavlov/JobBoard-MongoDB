﻿using AutoMapper;
using JobBoard.Common.Extensions;
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
    public class CandidateCvService : ICandidateCvService
    {
        private readonly JobBoardDbContext db;
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> userManager;

        public CandidateCvService(
            JobBoardDbContext db,
            IHttpContextAccessor context,
            UserManager<User> userManager
        )
        {
            this.db = db;
            _context = context;
            this.userManager = userManager;
        }

        public ObjectId GetLoggedUser()
        {
            return  userManager.GetUserId(_context.HttpContext.User).ToObjectId();
        }

        public IEnumerable<CvOverviewModel> GetAllCvsOfLoggedUser()
        {
            var cvs = this.db.Cvs.Find(c => c.UserId == GetLoggedUser())
                .ToEnumerable()
                .Reverse();

            var cvList = Mapper.Map<IEnumerable<CvOverviewModel>>(cvs);
  
            return cvList;
        }

        public string Add(CvCreateModel model)
        {
            var cv = Mapper.Map<Cv>(model);
            cv.UserId =  GetLoggedUser();
            cv.Id = ObjectId.GenerateNewId();

            this.db.Cvs.InsertOneAsync(cv);
            return cv.Id.ToString();
        }

        public string GetUserIdByCvId(string id)
            => this.db.Cvs.Find(c => c.Id == id.ToObjectId()).SingleOrDefault().UserId.ToString();

        public CvPreviewModel GetCvDetails(string id)
        {
            var cv = this.db.Cvs.Find(c => c.Id == id.ToObjectId()).SingleOrDefault();
            var personalInfoEditModel = Mapper.Map<CvPreviewModel>(cv);

            return personalInfoEditModel;
        }

        public PersonalInfoEditModel GetPersonalInfoById(string id)
        {
            var personalInfo =  this.db.Cvs.Find(c => c.Id == id.ToObjectId()).SingleOrDefault().PersonalInfo;
            var personalInfoEditModel = Mapper.Map<PersonalInfoEditModel>(personalInfo);

            return personalInfoEditModel;
        }

        public void UpdatePersonalInfo(string id, PersonalInfoEditModel form)
        {
            var personalInfo = Mapper.Map<PersonalInfo>(form);

            var updateResult = this.db.Cvs.UpdateOne(
                    Builders<Cv>.Filter.Eq("_id", id.ToObjectId()),
                    Builders<Cv>.Update.Set(nameof(Cv.PersonalInfo), personalInfo));
        }

        public bool CvBelongsToLoggedUser(string cvId)
        {
            string cvUserId = GetUserIdByCvId(cvId);
            string userId = GetLoggedUser().ToString();

            if(cvUserId == null || cvUserId != userId)
            {
                return false;
            }
            return true;
        }

        public void Delete(string id)
        {
              this.db.Cvs.DeleteOne(c => c.Id == id.ToObjectId());
        }
    }
}
