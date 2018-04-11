using JobBoard.Services.Candidates.Models.Jobs;

namespace JobBoard.Services.Candidates
{
    public interface ICandidateJobService
    {
        JobListModel GetAllJobs(int page = 1);
        JobListModel GetSearchedJobs(string text, int page = 1);
        JobDetailsModel GetJobDetails(string id);
        bool ApplyCvToJob(JobDetailsModel model, string id);
    }
}
