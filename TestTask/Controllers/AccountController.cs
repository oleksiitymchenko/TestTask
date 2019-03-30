using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTask.ViewModels;
using TestTask.DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestTask.Services;

namespace TestTask.Controllers
{
    public class AccountController : Controller
    {
        private AccountService service;

        public AccountController(AccountService service)
        {
            this.service = service;
            ViewData["IsLoggedIn"] = false;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["IsLoggedIn"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await service.FindUserByCredentialsAsync(model);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect data");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["IsLoggedIn"] = false;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var isNewUser = await service.FindAndAddAsync(model);

                if (!isNewUser) ModelState.AddModelError("", "Such user exists");

                await Authenticate(model.Email); 
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
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
        }
    }
}