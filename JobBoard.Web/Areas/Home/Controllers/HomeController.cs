using JobBoard.Data.Models;
using JobBoard.Web.Areas.Candidate.Controllers;
using JobBoard.Web.Areas.Employer.Controllers;
using JobBoard.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static JobBoard.Web.Infrastructure.Constants.Web;


namespace JobBoard.Web.Areas.Home.Controllers
{
    [Area(HomeArea)]
//    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private SignInManager<User> signInManager;

        public HomeController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Route("/")]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User) && User.IsInRole(EmployerRole))
            {
                return this.RedirectToAction<Employer.Controllers.JobsController>(nameof(Employer.Controllers.JobsController.List));
            }
            else
            {
                return this.RedirectToAction<Candidate.Controllers.JobsController>(nameof(Candidate.Controllers.JobsController.All));
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
