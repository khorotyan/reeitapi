using Microsoft.Extensions.Configuration;
using ReeitApi.Entities;
using ReeitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.BBL
{
    public interface IAccountsBBL
    {
        Task<List<Account>> GetAccounts();
    }

    public class AccountsBBL : IAccountsBBL
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountsRepo _accountsRepo;

        public AccountsBBL(IConfiguration configuration, IAccountsRepo accountsRepo)
        {
            _configuration = configuration;
            _accountsRepo = accountsRepo;
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _accountsRepo.GetAccountsAsync();
        }
    }
}
