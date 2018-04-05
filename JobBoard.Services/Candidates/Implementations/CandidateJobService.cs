using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Data;
using JobBoard.Data.Models;
using JobBoard.Data.Models.Employers;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using static JobBoard.Services.Constants.PageSettings;


namespace JobBoard.Services.Candidates.Implementations
{
    public class CandidateJobService : ICandidateJobService
    {

        private readonly JobBoardDbContext db;
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> userManager;

        public CandidateJobService(
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
            return userManager.GetUserId(_context.HttpContext.User);
        }

        public JobListModel GetAllJobs(int page = 1)
        {
            var jobs = this.db.Jobs.AsQueryable();

            var jobPage = new JobListModel
            {
                Jobs = jobs
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                        .AsQueryable()
                        .ProjectTo<JobModel>(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(jobs.Count() / (double)PageSize)
            };

            return jobPage;
        }

        public JobDetailsModel GetJobDetails(string id)
        {
            var userId = ObjectId.Parse(GetLoggedUser());
            var job = this.db.Jobs.Find(j => j.Id == ObjectId.Parse(id)).SingleOrDefault();
            var jobDetails = Mapper.Map<JobDetailsModel>(job);

            var cvs = this.db.Cvs.Find(c => c.UserId == userId).ToList();
            jobDetails.Cvs = Mapper.Map<IEnumerable<CvOverviewModel>>(cvs);

            return jobDetails;
        }

        public bool ApplyCvToJob(JobApplicationModel model, string id)
        {
            var jobApplication = Mapper.Map<JobApplication>(model);
            var job = this.db.Jobs.Find(j => j.Id == ObjectId.Parse(id)).SingleOrDefault();

            bool applicationExists = job.Applications.Select(a => a.AppliedCvId).Equals(model.AppliedCvId);

            if(!applicationExists)
            {
                var updateResult = this.db.Jobs.UpdateOne(
                    Builders<Job>.Filter.Eq("_id", ObjectId.Parse(id)),
                    Builders<Job>.Update.AddToSet(nameof(Job.Applications), jobApplication));

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
