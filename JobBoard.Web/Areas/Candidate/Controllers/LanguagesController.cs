using JobBoard.Services.Candidates;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    public class LanguagesController : CvGenericListCrudController<LanguageListModel>
    {
        public LanguagesController(ICandidateCvService cvs, ILanguageService repo) : base(cvs)
        {
            this.repo = repo;
        }

        public override IActionResult RedirectToActionOnSave(string id)
        {
           return this.RedirectToAction<SkillsController>(nameof(SkillsController.Edit), id);
        }
    }
}
