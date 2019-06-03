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
    public interface IUsersRepo
    {
        Task<User> GetUserAsync(string username);
    }

    public class UsersRepo : IUsersRepo
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public UsersRepo(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
    }
}
