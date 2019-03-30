using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.DataAccess.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Title"] = "Title";
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["IsLoggedIn"] = true;
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
