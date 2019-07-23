using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using asp_xamar_solution.Models.NonQuaryableModels;
using Microsoft.AspNetCore.Identity;
using asp_xamar_solution.CommonFunctions;
using Microsoft.Extensions.Configuration;

namespace asp_xamar_solution.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration config;

        public AccountController(ApplicationDBContext _context, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IConfiguration _config)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            config = _config;
        }

        // Registration
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody]RegDataInput RegData)
        {
            IdentityUser user = new IdentityUser { UserName = RegData.UserName, Email = RegData.Email };
            IdentityResult result = await userManager.CreateAsync(user, RegData.Password);
            if (result.Succeeded)
            {
                CreateCoinDataAsync createCoin = new CreateCoinDataAsync();
                await createCoin.CreateCoinData(context, RegData);

                return Ok(new { result = true });
            }
            else
            {
                return Ok(new { result = false });
            }
        }

        // Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginData)
        {
            var user = await userManager.FindByEmailAsync(loginData.Email);
            if (user == null)
            {
                return Ok(new { error = "User not found" });
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, loginData.Password, loginData.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ISecurityToken securityToken = new SecurityToken();
                string tokenString = securityToken.CreateToken(config);
                return Ok(new { result = result.Succeeded, token = tokenString, useremail = loginData.Email, username = user.UserName });
            }
            else
            {
                return Ok(new { result = result.Succeeded });
            }
        }
    }
}