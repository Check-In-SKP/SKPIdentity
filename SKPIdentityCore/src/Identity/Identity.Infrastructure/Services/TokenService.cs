using Identity.Application.Common.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configration;
        private readonly RSA _rsa;

        public TokenService(IConfiguration configuration)
        {
            _configration = configuration;
            _rsa = RSA.Create();
        }

        // Using Hmac encryption for internal authentication only (more performance freindly)
        public string GenerateIdToken(IEnumerable<Claim> claims, int expiryInMinutes)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["JwtSettings:Secret"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configration["JwtSettings:Issuer"],
                audience: _configration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        // Uses RSA encryption for 3rd party authentication
        public string GenerateAccessToken(IEnumerable<Claim> claims, int expiryInMinutes)
        {
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256);
            var jwt = new JwtSecurityToken(
                issuer: _configration["JwtSettings:Issuer"],
                audience: _configration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
