using JobBoard.Services.Candidates.Models.Jobs;

namespace JobBoard.Services.Candidates
{
    public interface ICandidateJobService
    {
        JobListModel GetAllJobs(int page = 1);
        JobDetailsModel GetJobDetails(string id);
        bool ApplyCvToJob(JobApplicationModel model, string id);
    }
}
