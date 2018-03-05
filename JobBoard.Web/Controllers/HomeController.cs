using JobBoard.Data;
using JobBoard.Data.Models;
using JobBoard.Data.Models.Cvs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobBoardDbContext db;

        public HomeController(JobBoardDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            //var work = new Work
            //{
            //    Employer = "Nestle",
            //    Position = "Package Manager"
            //};

            //var work2 = new Work();
            //work2.Employer = "AT";
            //work2.Position = "Acc Representative";

            //var cv = new Cv();
            //cv.Title = "TestMongoCv";
            //cv.Works.Add(work);
            //cv.Works.Add(work2);

            //db.Cvs.InsertOne(cv);
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
