using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobBoard.Data;
using JobBoard.Data.Models;
using JobBoard.Data.Models.Cvs;
using JobBoard.Data.Models.Employers;
using JobBoard.Services.Employers.Models.Cvs;
using JobBoard.Services.Employers.Models.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using static JobBoard.Services.Constants.PageSettings;

namespace JobBoard.Services.Employers.Implementations
{
    public class EmployerJobService : IEmployerJobService
    {
        private readonly JobBoardDbContext db;
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> userManager;

        public EmployerJobService(
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

        public string AddJob(JobCreateModel form)
        {
            var job = Mapper.Map<Job>(form);
            job.UserId = ObjectId.Parse(GetLoggedUser());
            job.Id = ObjectId.GenerateNewId();

            this.db.Jobs.InsertOneAsync(job);
            return job.Id.ToString();
        }

        public JobListModel GetUserJobs(int page = 1)
        {
            var userId = ObjectId.Parse(GetLoggedUser());
            var jobs = this.db.Jobs.Find(c => c.UserId == userId).ToEnumerable();
 
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
            var job = this.db.Jobs.Find(j => j.Id == ObjectId.Parse(id) && j.UserId == userId).SingleOrDefault();

            var applicationIds = job.Applications.Select( a => a.AppliedCvId);   
            var filter = Builders<Cv>.Filter.In(c => c.Id, applicationIds);
            var cvs = this.db.Cvs.Find(filter).ToList();

            var jobDetails = Mapper.Map<JobDetailsModel>(job);
            var cvModels = Mapper.Map<List<CvOverviewModel>>(cvs);
            jobDetails.Cvs.AddRange(cvModels);

            return jobDetails;
        }

        public CvPreviewModel GetCvDetailsOfJob(string jobId, string cvId)
        {
            var userId = ObjectId.Parse(GetLoggedUser());
            var job = this.db.Jobs.Find(j => j.Id == ObjectId.Parse(jobId)).SingleOrDefault();

            bool jobBelongsToUser = (job.UserId == userId);
            if (jobBelongsToUser)
            {
                bool jobContainsCv = job.Applications.Select(a => a.AppliedCvId).Contains(ObjectId.Parse(cvId));
                if (jobContainsCv)
                {
                    var cv = this.db.Cvs.Find(c => c.Id == ObjectId.Parse(cvId)).SingleOrDefault();
                    var cvDetails = Mapper.Map<CvPreviewModel>(cv);
                    cvDetails.MotivationalLetter = job.Applications.Where(a => a.AppliedCvId.ToString() == cvId).SingleOrDefault().MotivationalLetter;
                    return cvDetails;
                }
            }
            return null;
        }
    }
}
