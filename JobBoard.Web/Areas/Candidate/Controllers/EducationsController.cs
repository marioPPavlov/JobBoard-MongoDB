using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    public class EducationsController : CvGenericListCrudController<EducationListModel>
    {
        public EducationsController(ICandidateCvService cvs, IEducationService repo) : base(cvs)
        {
            this.repo = repo;
        }

        public override IActionResult RedirectToActionOnSave(string id)
        {
           return this.RedirectToAction<LanguagesController>(nameof(LanguagesController.Edit), id);
        }
    }
}
