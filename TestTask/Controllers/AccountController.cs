using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTask.ViewModels;
using TestTask.DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestTask.Services;
using Microsoft.Extensions.Logging;

namespace TestTask.Controllers
{
    public class AccountController : Controller
    {
        private ILogger logger;
        private AccountService service;

        public AccountController(AccountService service, ILogger<AccountController> logger)
        {
            this.service = service;
            this.logger = logger;
            ViewData["IsLoggedIn"] = false;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            logger.LogInformation($"Executing Account/Login");

            ViewData["IsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            logger.LogInformation($"Starting Login procedure");

            ViewData["IsLoggedIn"] = false;
            if (ModelState.IsValid)
            {
                User user = await service.FindUserByCredentialsAsync(model);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    logger.LogInformation($"User {user.Email} successfuly logged in");

                    return RedirectToAction("Index", "Home");
                }
                logger.LogWarning($"User {user.Email} didn`t logged in");
                ModelState.AddModelError("", "Incorrect data");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            logger.LogInformation($"Executing Account/Register");
            ViewData["IsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            logger.LogInformation($"Starting Login procedure");

            ViewData["IsLoggedIn"] = true;
            if (ModelState.IsValid)
            {
                var isNewUser = await service.FindAndAddAsync(model);

                if (!isNewUser) ModelState.AddModelError("", "Such user exists");

                await Authenticate(model.Email);
                logger.LogInformation($"User {model.Email} successfuly registered");
                return RedirectToAction("Index", "Home");
            }
            logger.LogWarning($"User {model.Email} didn`t registered");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation($"Logout {User.Identity.Name}");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
         
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
           
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            logger.LogInformation($"Authenticated {userName}");
        }
    }
}