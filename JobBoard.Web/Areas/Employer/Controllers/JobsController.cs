using JobBoard.Services.Employers;
using JobBoard.Services.Employers.Models.Jobs;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Areas.Employer.Controllers
{
    [Area(EmployersArea)]
    [Route("[area]/[controller]/[action]/{id?}")]
    [Authorize(Roles = EmployerRole)]
    public class JobsController : Controller
    {
        private readonly IEmployerJobService emp;

        public JobsController
            (
            IEmployerJobService emp
            )
        {
            this.emp = emp;
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public IActionResult Create(JobCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                TempData.AddSuccessMessage("Job successfully created");
                var id = this.emp.AddJob(model).ToString();
                return this.RedirectToAction(nameof(List));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public IActionResult List(int page = 1)
        {
            var jobPageListModel = emp.GetUserJobs();

            return this.View(jobPageListModel);
        }

        public IActionResult Details(string id)
        {
            var jobDetails = this.emp.GetJobDetails(id);

            if(jobDetails == null)
            {
                return BadRequest();
            }
            return View(jobDetails);
        }

        public IActionResult ViewCv(string id, string cvId)
        {
            var cvDetails = this.emp.GetCvDetailsOfJob(id, cvId);

            if (cvDetails == null)
            {
                return BadRequest();
            }
            return View(cvDetails);
        }

    }
}
