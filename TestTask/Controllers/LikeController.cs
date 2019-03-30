using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers
{
    [Route("[controller]")]
    public class LikeController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("LikePage/{pageName}")]
        public IActionResult LikePage(string pageName)
        {
            if (pageName=="kek")
            {
                Console.WriteLine("asdsadasds");
            }
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("DisLikePage/{pageName}")]
        public ActionResult DisLikePage(string pageName)
        {
            return Ok();
        }
    }
}