using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReeitApi.Entities;
using ReeitApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.Repositories
{
    public interface IAccountsRepo
    {
        Task<List<Account>> GetAccountsAsync();
    }

    public class AccountsRepo : IAccountsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AccountsRepo(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }
    }
}
