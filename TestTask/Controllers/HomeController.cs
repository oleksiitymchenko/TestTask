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
            ViewData["IsLiked"] = isLiked;
            ViewData["Title"] = "Index";
            return View();
        }

        public async Task<IActionResult> About()
        {
            var page = await service.CreateIfNotExists("About");
            var isLiked = await service.CheckIsLiked("About", User.Identity.Name);

            ViewData["TotalLikes"] = page.Likes;
            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["IsLiked"] = isLiked;
            ViewData["Title"] = "About";

            ViewData["Message"] = "Truly new description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var page = await service.CreateIfNotExists("Contact");
            var isLiked = await service.CheckIsLiked("Contact", User.Identity.Name);

            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["TotalLikes"] = page.Likes;
            ViewData["IsLiked"] = isLiked;
            ViewData["Title"] = "Contact";


            ViewData["Message"] = "Truly new contact page.";

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var page = await service.CreateIfNotExists("Privacy");
            var isLiked = await service.CheckIsLiked("Privacy", User.Identity.Name);

            ViewData["IsLoggedIn"] = User.Identity.IsAuthenticated;
            ViewData["TotalLikes"] = page.Likes;
            ViewData["IsLiked"] = isLiked;
            ViewData["Title"] = "Privacy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
