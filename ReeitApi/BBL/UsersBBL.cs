using Microsoft.Extensions.Configuration;
using ReeitApi.Entities;
using ReeitApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.BBL
{
    public interface IUsersBBL
    {
        Task<User> GetUser(string username);
    }

    public class UsersBBL : IUsersBBL
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepo _usersRepo;

        public UsersBBL(IConfiguration configuration, IUsersRepo usersRepo)
        {
            _configuration = configuration;
            _usersRepo = usersRepo;
        }

        public async Task<User> GetUser(string username)
        {
            return await _usersRepo.GetUserAsync(username);
        }
    }
}
