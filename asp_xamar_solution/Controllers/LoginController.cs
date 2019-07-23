using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using asp_xamar_solution.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace asp_xamar_solution.Controllers
{
    public class LoginController : Controller
    {
        private LoginModel loginData = new LoginModel();
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public LoginController(SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> manager)
        {
            signInManager = _signInManager;
            userManager = manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(loginData);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);
            if (user == null)
            {
                // The user don't excists actually can be handlled better
                return NotFound();
            }


            var result = await signInManager.PasswordSignInAsync(user.UserName, data.Password, data.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                return RedirectToAction("Main", "Wallet");
            }
            else
            {
                data.Password = null;
                data.LoginError = "Invalid Email or Password";
                return View(data);
            }
        }
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("WelcomeAction", "Welcome");
        }
    }
}