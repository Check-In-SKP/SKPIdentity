using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Models;
using Identity.SharedKernel.Models.Enums;
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

        // Uses Hmac signing encryption for internal authentication (more performance friendly)
        public async Task<string> GenerateHmacToken(IEnumerable<Claim> claims, int expiryInMinutes, TokenType tokenType)
        {
            var hmacKey = await _keyManager.HmacKeyAsync;

            // Add TokenType claim
            claims = claims.Concat(new[] { new Claim("TokenType", tokenType.ToTokenString()) });

            var key = new SymmetricSecurityKey(hmacKey);
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

        // Uses RSA signing encryption for 3rd party authentication
        public async Task<string> GenerateRsaToken(IEnumerable<Claim> claims, int expiryInMinutes, TokenType tokenType)
        {
            var rsaKey = await _keyManager.GetPrivateRsaSecurityKeyAsync();

            // Add TokenType claim
            claims = claims.Concat(new[] { new Claim("token_type", tokenType.ToTokenString()) });

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

        // Generates a random string
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
