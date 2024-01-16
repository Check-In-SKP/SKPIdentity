using Identity.Application.Common.Services.Interfaces;
using Identity.Infrastructure.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.OAuthService
{
    public class AuthService : IAuthService
    {
        // Method to validate PKCE code_verifier
        public bool ValidateCodeVerifier(AuthCode code, string codeVerifier)
        {
            var challengeBytes = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
            return code.CodeChallenge == WebEncoders.Base64UrlEncode(challengeBytes);
        }
    }
}
