using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Cvs;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Web.Infrastructure.Extensions;
using JobBoard.Services.Candidates.Models.Cvs.Lists;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    public class SkillsController : CvGenericListCrudController<SkillListModel>
    {
        public SkillsController(ICandidateCvService cvs, ISkillService repo) : base(cvs)
        {
            this.repo = repo;
        }

        public override IActionResult RedirectToActionOnSave(string id)
        {
            return this.RedirectToAction<CvsController>(nameof(CvsController.Details), id);
        }
    }
}
