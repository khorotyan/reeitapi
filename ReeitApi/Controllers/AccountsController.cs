using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReeitApi.BBL;
using ReeitApi.Entities;

namespace ReeitApi.Controllers
{
    [Produces("application/json")]
    [Route("accounts")]
    public class AccountsController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountsBBL _accountsBBL;
        
        public AccountsController(IConfiguration configuration, IAccountsBBL accountsBBL)
        {
            _configuration = configuration;
            _accountsBBL = accountsBBL;
        }

        [AllowAnonymous]
        [HttpPost("register")] 
        public async Task<UserToken> Register([FromBody] RegistrationUser registrationUser)
        {
            return await _accountsBBL.Register(registrationUser);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<UserToken> Login(string username, string email, string password)
        {
            return await _accountsBBL.Login(username, email, password);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<object> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
