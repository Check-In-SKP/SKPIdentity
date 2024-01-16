﻿using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Models;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infrastructure.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IKeyManager _keyManager;
        private readonly JwtSettings _jwtSettings;

        public TokenProvider(IOptions<JwtSettings> jwtSettings, IKeyManager keyManager)
        {
            _jwtSettings = jwtSettings.Value;
            _keyManager = keyManager;
        }

        // Uses Hmac encryption for internal authentication (more performance friendly)
        public string GenerateIdToken(IEnumerable<Claim> claims, int expiryInMinutes)
        {
            // Get's key from KeyManager
            var hmacKey = _keyManager.HmacKeyAsync.ToString() ?? throw new InvalidOperationException("Key data is null.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        // Uses RSA encryption for 3rd party authentication
        public async Task<string> GenerateAccessToken(IEnumerable<Claim> claims, int expiryInMinutes)
        {
            var rsaKey = await _keyManager.GetPublicKeyAsync();
            var signingCredentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);

            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
