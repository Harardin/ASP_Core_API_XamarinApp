using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models;
using asp_xamar_solution.Models.NonQuaryableModels;
using asp_xamar_solution.CommonFunctions;
using Microsoft.AspNetCore.Authorization;

namespace asp_xamar_solution.Controllers
{
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        private RegDataInput DataInput = new RegDataInput();
        private ApplicationDBContext context;
        public RegistrationController(ApplicationDBContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(DataInput);
        }
        
        [HttpPost]
        public IActionResult Registration(RegDataInput DT)
        {
            RegistrationFunction registration = new RegistrationFunction();
            if (DT.UserName != null && DT.Email != null && DT.Password != null && DT.ConfPassword != null)
            {
                if (DT.Email.Contains('@') && DT.Email.Contains('.'))
                {
                    if (registration.Registration(context, DT))
                    {
                        // Return View That Say that all cool and you can try to login
                        return View("RegSucces");
                    }
                    else
                    {
                        // And edding the errors about Email or other
                        return View(DT);
                    }
                }
                else
                {
                    // And edding the errors about Email or other
                    return View(DT);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DT compare wrong" + DT.Email + "Password " + DT.Password);
                // Some fields waren't filled
                return View(DT);
            }
        }
    }
}