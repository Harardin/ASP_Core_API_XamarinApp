using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using asp_xamar_solution.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace asp_xamar_solution.Controllers
{ 
    public class LoginController : Controller
    {
        private LoginModel loginData = new LoginModel();
        private IUserDataModel qData;
        public LoginController(IUserDataModel dt)
        {
            qData = dt;
        }

        private List<Claim> claims;
        private AuthenticationProperties authProperties;
        private ClaimsIdentity claimsIdentity;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(loginData);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (login.Email.Contains('@') && login.Password != null && qData.QUserData.Any(user => user.Email == login.Email && user.Paswword == login.Password))
            {
                string Name = qData.QUserData.Where(q => q.Email == login.Email).Select(q => q.UserName).SingleOrDefault();

                claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, Name),
                    new Claim(ClaimTypes.Email, login.Email),
                };
                authProperties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(100),
                    IsPersistent = login.RememberMe
                };
                claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                Task.Run(async () => await SignAsync()).Wait();

                // login was succesfull
                return RedirectToAction("Main", "Wallet");
            }
            else
            {
                login.Password = null;
                login.LoginError = "Invalid Email or Password";
                return View(login);
            }
        }
        [Authorize]
        public IActionResult SignOut()
        {
            Task.Run(async () => await OutAsync()).Wait();
            return RedirectToAction("WelcomeAction", "Welcome");
        }
        private async Task SignAsync()
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        private async Task OutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}