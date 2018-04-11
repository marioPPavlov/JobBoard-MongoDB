using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Common.Extensions;
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

        public ObjectId GetLoggedUser()
        {
            return userManager.GetUserId(_context.HttpContext.User).ToObjectId();
        }

        public JobListModel GetAllJobs(int page = 1)
        {
            var jobs = this.db.Jobs.AsQueryable().ToList();

            return GetPagedListFromJobs(jobs,page);
        }

        public JobListModel GetSearchedJobs(string text, int page = 1)
        { 
            var jobs = this.db.Jobs.AsQueryable().ToList();
            var searchWords = text.ToLower().Split(' ').ToList();

            var foundJobs = jobs.
                            .Where(j =>
                                   j.Tags.ToLower().Split(' ').Any(t => searchWords.Any(s => t.Contains(s)))
                                   ||
                                   j.Title.ToLower().Split(' ').Any(t => searchWords.Any(s => t.Contains(s))) );

            return GetPagedListFromJobs(foundJobs, page);
        }

        private JobListModel GetPagedListFromJobs(IEnumerable<Job> jobs, int page)
        {
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
            var userId = GetLoggedUser();
            var job = this.db.Jobs.Find(j => j.Id == id.ToObjectId()).SingleOrDefault();
            var cvs = this.db.Cvs.Find(c => c.UserId == userId).ToList();

            if (job == null || cvs == null)
            {
                return null;
            }

            var jobDetails = Mapper.Map<JobDetailsModel>(job);
            jobDetails.Cvs = Mapper.Map<IEnumerable<CvOverviewModel>>(cvs);

            return jobDetails;
        }

        public bool ApplyCvToJob(JobDetailsModel model, string id)
        {
            var jobApplication = Mapper.Map<JobApplication>(model);
            var job = this.db.Jobs.Find(j => j.Id == id.ToObjectId()).SingleOrDefault();
            if (job!=null)
            {
                bool applicationExists = job.Applications.Select(a => a.AppliedCvId).Contains(model.AppliedCvId.ToObjectId());
                if (!applicationExists)
                {
                    var updateResult = this.db.Jobs.UpdateOne(
                        Builders<Job>.Filter.Eq("_id", id.ToObjectId()),
                        Builders<Job>.Update.AddToSet(nameof(Job.Applications), jobApplication));

                    return true;
                }
            }
            return false;
        }


    }
}
