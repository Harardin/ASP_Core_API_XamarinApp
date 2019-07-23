using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asp_xamar_solution.Models.NonQuaryableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace asp_xamar_solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessTestController : ControllerBase
    {

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("PrivateResponce")]
        [HttpGet]
        public async Task<IActionResult> PrivateResponce()
        {
            return Ok(new { message = "This is info only for Authorized users" });
        }
    }
}