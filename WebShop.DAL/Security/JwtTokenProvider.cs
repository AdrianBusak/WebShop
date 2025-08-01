﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebShop.DAL.Security
{
    public class JwtTokenProvider
    {
        public static string CreateToken(string secureKey, int expiration, string subject = null, string role = null)
        {
            var tokenKey = Encoding.UTF8.GetBytes(secureKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(expiration),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (!string.IsNullOrEmpty(subject))
            {
                tokenDescriptor.Subject = new ClaimsIdentity(new Claim[]
                {   
                    new Claim(ClaimTypes.Name, subject),
                    new Claim(JwtRegisteredClaimNames.Sub, subject),
                    new Claim(ClaimTypes.Role, role),
                });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);

            return serializedToken;
        }
    }
}
