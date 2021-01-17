using Microsoft.Extensions.Configuration;
using ReeitApi.Entities;
using ReeitApi.Enums;
using ReeitApi.Repositories;
using ReeitApi.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ReeitApi.BBL
{
    public interface IAccountsBBL
    {
        Task<UserToken> Register(RegistrationUser registrationUser);
        Task<UserToken> Login(string username, string email, string password);
    }

    public class AccountsBBL : IAccountsBBL
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountsRepo _accountsRepo;

        private const string WRONG_EMAIL_MESSAGE = "Email or password is incorrect";
        private const string WRONG_USERNAME_MESSAGE = "Username or password is incorrect"; 

        public AccountsBBL(IConfiguration configuration, IAccountsRepo accountsRepo)
        {
            _configuration = configuration;
            _accountsRepo = accountsRepo;
        }
         
        public async Task<UserToken> Register(RegistrationUser registrationUser)
        {
            User user = registrationUser.User;
            Account account = registrationUser.Account;

            bool usernameExists = await _accountsRepo.UsernameExistsAsync(user.Username);
            if (usernameExists) throw new ApiException(ErrorCode.BadRequest, "Username exists");

            bool emailExists = await _accountsRepo.EmailExistsAsync(account.Email);
            if (emailExists) throw new ApiException(ErrorCode.BadRequest, "Email exists");

            account.Password = PasswordHasher.Hash(account.Password);
            await _accountsRepo.CreateAccountAsync(account);

            user.Id = account.Id;
            await _accountsRepo.CreateUserAsync(user);

            var jwtClaim = new JwtClaim() { UserId = user.Id, Username = user.Username };
            var tokenInfo = Authentication.GenerateToken(jwtClaim);

            return new UserToken
            {
                User = user,
                TokenInfo = tokenInfo
            };
        }

        public async Task<UserToken> Login(string username, string email, string password)
        {
            User user = new User();
            Account account = null;

            if (username != null)
            {
                user = await _accountsRepo.GetUserByUsername(username);
                account = await _accountsRepo.GetAccountById(user.Id);
            }

            if (email != null)
            {
                account = await _accountsRepo.GetAccountByEmail(email);
            }

            if (account == null)
            {
                string errorMessage = username == null ? WRONG_EMAIL_MESSAGE : WRONG_USERNAME_MESSAGE;
                throw new ApiException(ErrorCode.BadRequest, errorMessage);
            }

            bool verified = PasswordHasher.Verify(account.Password, password);

            if (!verified)
            {
                string errorMessage = username == null ? WRONG_EMAIL_MESSAGE : WRONG_USERNAME_MESSAGE;
                throw new ApiException(ErrorCode.BadRequest, errorMessage);
            }

            var jwtClaim = new JwtClaim() { UserId = user.Id, Username = user.Username };
            var tokenInfo = Authentication.GenerateToken(jwtClaim);

            return new UserToken
            {
                User = user,
                TokenInfo = tokenInfo
            };
        }
    }
}
