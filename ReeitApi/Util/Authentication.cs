using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReeitApi.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReeitApi.Util
{
    public class Authentication
    {
        private const int EXPIRATION_MINUTES = 60;

        public static IConfiguration configuration;
        public static readonly TimeSpan expirationTime = TimeSpan.FromMinutes(EXPIRATION_MINUTES);

        public static TokenInfo GenerateToken(JwtClaim claim)
        {
            string jsonClaim = JsonConvert.SerializeObject(claim);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jsonClaim),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["SigningKey"])), 
                SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.UtcNow.Add(expirationTime);

            var token = new JwtSecurityToken
                (
                    issuer: configuration["Issuer"],
                    audience: configuration["Audience"],
                    claims: claims,
                    expires: expirationDate,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: credentials
                );

            var tokenInfo = new TokenInfo()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationTime = expirationDate
            };

            return tokenInfo;
        }
    }
}
