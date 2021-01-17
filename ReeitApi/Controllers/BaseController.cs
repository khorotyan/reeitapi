using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReeitApi.Controllers
{
    [ApiController]
    //[Authorize]
    public class BaseController : ControllerBase
    {
        public int UserId => GetUserId();

        private int GetUserId()
        {
            return 1;
        }

        [HttpGet("healthcheck")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
