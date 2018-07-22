using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Web.Areas.Home.Controllers;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    [Area(CandidatesArea)]
    [Route("[area]/[controller]/[action]/{id?}")]
    [Authorize(Roles = CandidateRole)]
    public class CvsController : Controller
    {
        private readonly ICandidateCvService cvs;

        public CvsController
            (
            ICandidateCvService cvs
            )
        {
            this.cvs = cvs;
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            if (this.cvs.GetLoggedUser() == null)
            {
                this.RedirectToAction<AccountController>(nameof(AccountController.Register));
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CvCreateModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                TempData.AddSuccessMessage("Cv successfully created");
                var id = this.cvs.Add(model).ToString();
                return this.RedirectToAction(nameof(PersonalInfo), new { id });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public IActionResult Details(string id)
        {
            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                CvPreviewModel cvPreviewModel = this.cvs.GetCvDetails(id);
                return View(cvPreviewModel);
            }
            return BadRequest();
        }

        public IActionResult List(string id)
        {
            IEnumerable<CvOverviewModel> cvList = this.cvs.GetAllCvsOfLoggedUser();
            return View(cvList);
        }

        public IActionResult PersonalInfo(string id)
        {
            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                PersonalInfoEditModel personalInfoEditModel = this.cvs.GetPersonalInfoById(id);
                return View(personalInfoEditModel);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult PersonalInfo(string id, PersonalInfoEditModel form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }

            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                this.cvs.UpdatePersonalInfo(id, form);
                return this.RedirectToAction<WorksController>(nameof(WorksController.Edit), id);
            }
            return BadRequest();
        }

        public virtual IActionResult Delete(string id)
        {
            if (this.cvs.CvBelongsToLoggedUser(id))
            {
                this.cvs.Delete(id);
                TempData.AddSuccessMessage("Cv successfully deleted");

                return this.RedirectToAction(nameof(List));

            }
            return BadRequest();
        }
    }
}

