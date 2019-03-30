using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.DataAccess.Models;
using TestTask.Services;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private PageService service;

        public HomeController(PageService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var page = await service.CreateIfNotExists("Index");
            var isLiked = await service.CheckIsLiked("Index", User.Identity.Name);
            ViewData["TotalLikes"] = page.Likes;
            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["Title"] = "Title";
            return View();
        }

        public async Task<IActionResult> About()
        {
            var page = await service.CreateIfNotExists("About");

            ViewData["TotalLikes"] = page.Likes;

            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;

            ViewData["Message"] = "Truly new description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var page = await service.CreateIfNotExists("Contact");

            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["TotalLikes"] = 10;
            ViewData["Message"] = "Truly new contact page.";

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var page = await service.CreateIfNotExists("Privacy");

            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["TotalLikes"] = page.Likes;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
