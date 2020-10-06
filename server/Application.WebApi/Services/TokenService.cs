using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.WebApi.Services 
{
    public class TokenService
    {
        public static string GenerateToken()
        {
            string key = Settings.SecretToken;
            var tokenHandler = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var accessCredentials = new SigningCredentials(tokenHandler, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var JWT = new JwtSecurityToken(
                issuer: "proffy.com",
                expires: DateTime.Now.AddMinutes(15),
                audience: "common_user",
                signingCredentials: accessCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(JWT);
        }
    }
}
