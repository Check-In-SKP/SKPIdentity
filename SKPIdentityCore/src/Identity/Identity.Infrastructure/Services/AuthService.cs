using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Services.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        // Method to validate PKCE code_verifier
        public bool ValidateCodeVerifier(AuthCode code, string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            return code.CodeChallenge == WebEncoders.Base64UrlEncode(challengeBytes);
        }
    }
}
