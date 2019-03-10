using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<List<Account>> GetAccounts()
        {
            return await _accountsBBL.GetAccounts();
        }
    }
}
