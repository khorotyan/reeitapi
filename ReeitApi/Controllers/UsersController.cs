using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReeitApi.BBL;
using ReeitApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Controllers
{
    [Produces("application/json")]
    [Route("users")]
    public class UsersController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersBBL _usersBBL;

        public UsersController(IConfiguration configuration, IUsersBBL usersBBL)
        {
            _configuration = configuration;
            _usersBBL = usersBBL;
        }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<User> GetUser([FromRoute] string username)
        {
            return await _usersBBL.GetUser(username);
        }
    }
}
