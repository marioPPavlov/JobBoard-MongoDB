using JobBoard.Services.Employers.Models.Cvs;
using JobBoard.Services.Employers.Models.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Employers
{
    public interface IEmployerJobService
    {
        string AddJob(JobCreateModel form);
        JobListModel GetUserJobs(int page = 1);
        JobDetailsModel GetJobDetails(string id);
        CvPreviewModel GetCvDetailsOfJob(string jobId, string cvId);
    }
}
