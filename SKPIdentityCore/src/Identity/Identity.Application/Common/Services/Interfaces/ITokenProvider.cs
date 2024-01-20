using Identity.SharedKernel.Models.Enums;
using System.Security.Claims;

namespace Identity.Application.Common.Services.Interfaces
{
    public interface ITokenProvider
    {
        public Task<string> GenerateRsaToken(IEnumerable<Claim> claims, int expiryInMinutes, TokenType tokenType);
        public Task<string> GenerateHmacToken(IEnumerable<Claim> claims, int expiryInMinutes, TokenType tokenType);
        public string GenerateRefreshToken();
    }
}
