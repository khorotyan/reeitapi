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
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task CreateAccountAsync(Account account);
        Task CreateUserAsync(User user);
        Task<Account> GetAccountByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<Account> GetAccountById(int id);
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

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .Where(u => u.Username == username)
                .AnyAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Accounts
                .Where(a => a.Email == email)
                .AnyAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await _context.Accounts
                .Where(a => a.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _context.Accounts
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
