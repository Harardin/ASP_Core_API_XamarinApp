using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_xamar_solution.Controllers
{
    [AllowAnonymous]
    public class WelcomeController : Controller
    {
        public IActionResult WelcomeAction()
        {
            return View();
        }
    }
}