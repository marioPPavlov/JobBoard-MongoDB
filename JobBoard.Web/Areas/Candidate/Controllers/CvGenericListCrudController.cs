using JobBoard.Common.DTO;
using JobBoard.Services.Candidates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Areas.Candidate.Controllers
{
    [Area(CandidatesArea)]
    [Authorize(Roles = CandidateRole)]
    [Route("[area]/[controller]/[action]/{id?}/{number?}")]
    public abstract class CvGenericListCrudController<L>: Controller
    {
        protected readonly ICandidateCvService cvs;
        protected IGenericListCrudService<L> repo;

        public CvGenericListCrudController
            (
            ICandidateCvService cvs
            )
        {
            this.cvs = cvs;
        }

        public virtual IActionResult Edit(string id)
        {

            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                var works = this.repo.GetListModel(id);
                return View(works);
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual IActionResult Edit(string id, L form)
        {
            if(!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Edit), new { id });
            }
            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                this.repo.Update(id, form);
                return RedirectToActionOnSave(id);
            }
            return BadRequest();
        }

        public virtual IActionResult Add(string id)
        {
            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                this.repo.Add(id);
                return this.RedirectToAction(nameof(Edit), new { id });
            }
            return BadRequest();
        }

        public virtual IActionResult Remove(string id, int number)
        {
            if(this.cvs.CvBelongsToLoggedUser(id))
            {
                this.repo.Delete(id, number);
                return this.RedirectToAction(nameof(Edit), new { id });
            }
            return BadRequest();
        }

        public abstract IActionResult RedirectToActionOnSave(string id);
    }
}
