using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Web.Infrastructure.Extensions;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    [Area(CandidatesArea)]
    [Route("[area]/[controller]/[action]/{id?}")]
    [Authorize(Roles = CandidateRole)]
    public class JobsController : Controller
    {
        private readonly ICandidateJobService can;

        public JobsController
            (
            ICandidateJobService can
            )
        {
            this.can = can;
        }

        [AllowAnonymous]        
        public IActionResult All(int page = 1)
        {
            var jobPageListModel = can.GetAllJobs(page);
            return this.View(jobPageListModel);
        }

        [AllowAnonymous]
        public IActionResult Search([FromQuery]string text,int page = 1 )
        {
            var jobPageListModel = can.GetSearchedJobs(text ,page);
            return this.View(jobPageListModel);
        }

        public IActionResult Details(string id)
        {
            var jobDetails = this.can.GetJobDetails(id);

            if(jobDetails == null)
            {
                return BadRequest();
            }
            return View(jobDetails);
        }

        [HttpPost]
        public IActionResult Details(JobDetailsModel form, string id)
        {
            if (!ModelState.IsValid || form.AppliedCvId == null)
            {
                TempData.AddErrorMessage("Please select a CV before applying");
                var jobDetails = this.can.GetJobDetails(id);
                jobDetails.MotivationalLetter = form.MotivationalLetter;
                jobDetails.AppliedCvId = form.AppliedCvId;
                return View(jobDetails);
            }

            var success = this.can.ApplyCvToJob(form, id);
            if (!success)
            {
                TempData.AddErrorMessage("You have already applied for this job with this CV");
                return this.RedirectToAction<JobsController>(nameof(JobsController.Details), id);
            }
            else
            {
                TempData.AddSuccessMessage("You have successfully applied");
                return this.RedirectToAction<JobsController>(nameof(JobsController.All));
            }
        }
    }
}
