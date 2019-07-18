using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using asp_xamar_solution.Models.NonQuaryableModels;
using asp_xamar_solution.CommonFunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace asp_xamar_solution.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        private RegDataInput DataInput = new RegDataInput();
        private readonly ApplicationDBContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RegistrationController(ApplicationDBContext _context, UserManager<IdentityUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(DataInput);
        }
        
        // This is using the ASP.NET Identity
        [HttpPost]
        public async Task<IActionResult> Registration(RegDataInput data)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = data.UserName, Email = data.Email };
                // This one stores the password as an MD5 Hash Aditional field as not encrypted password can be added
                IdentityResult result = await userManager.CreateAsync(user, data.Password);
                if (result.Succeeded)
                {
                    CreateCoinDataAsync createCoin = new CreateCoinDataAsync();
                    await createCoin.CreateCoinData(context, data);
                    return View("RegSucces");
                }
                else
                {
                    foreach(var er in result.Errors)
                    {
                        // This will show the regestration error to user
                        ModelState.AddModelError("", er.Description);
                    }
                    return View(data);
                }
            }
            else
            {
                return View(data);
            }
        }
    }
}