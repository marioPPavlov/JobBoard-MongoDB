using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("/")]
        public IActionResult All(int page = 1)
        {
            var jobPageListModel = can.GetAllJobs(page);

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
        public IActionResult Apply(JobApplicationModel form, string id)
        {
            this.can.ApplyCvToJob(form, id);
            return RedirectToAction("/");
        }
    }
}
