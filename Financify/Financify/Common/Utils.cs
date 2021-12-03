using Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Financify.Common
{
    public class Utils
    {
        private IConfiguration configuration;
        public Utils(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string BuildToken(User user)
        {
            var claims = new[]
                 {
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Issuer = configuration["Jwt:Issuer"] + "";

            var tokenString = new JwtSecurityToken(Issuer,
            Issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(tokenString);
            return tokenStr;
        }
    }
}
