using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Services;

namespace TestTask.Controllers
{
    [Route("[controller]")]
    public class LikeController : Controller
    {
        private ILogger logger;
        private PageService service;
        public LikeController(PageService service, ILogger<LikeController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("LikePage/{pageName}")]
        public async Task<IActionResult> LikePage(string pageName)
        {
            logger.LogInformation($"Executing LikePage/{pageName}");
            int likes = await service.LikePage(pageName, User.Identity.Name, 1);
            return Ok(likes);
        }

        [Authorize]
        [HttpGet]
        [Route("DisLikePage/{pageName}")]
        public async Task<IActionResult> DisLikePage(string pageName)
        {
            logger.LogInformation($"Executing DisLikePage/{pageName}");
            int likes = await service.LikePage(pageName, User.Identity.Name, -1);
            return Ok(likes);
        }
    }
}