using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    public class WorksController : CvGenericListCrudController<WorkListModel>
    {
        public WorksController(ICandidateCvService cvs, IWorkService repo) : base(cvs)
        {
            this.repo = repo;
        }

        public override IActionResult RedirectToActionOnSave(string id)
        {
            return this.RedirectToAction<EducationsController>(nameof(EducationsController.Edit), id);
        }
    }
}
