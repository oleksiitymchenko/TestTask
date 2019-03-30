using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Services;

namespace TestTask.Controllers
{
    [Route("[controller]")]
    public class LikeController : Controller
    {
        private PageService service;
        public LikeController(PageService service)
        {
            this.service = service;
        }

        [Authorize]
        [HttpGet]
        [Route("LikePage/{pageName}")]
        public async Task<IActionResult> LikePage(string pageName)
        {
            int likes = await service.LikePage(pageName, User.Identity.Name, 1);
            return Ok(likes);
        }

        [Authorize]
        [HttpGet]
        [Route("DisLikePage/{pageName}")]
        public async Task<IActionResult> DisLikePage(string pageName)
        {
            int likes = await service.LikePage(pageName, User.Identity.Name, -1);

            return Ok(likes);
        }
    }
}