using JobBoard.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static JobBoard.Web.Infrastructure.Constants.Web;

namespace JobBoard.Web.Areas.Home.Controllers
{
    [Area(HomeArea)]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
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
