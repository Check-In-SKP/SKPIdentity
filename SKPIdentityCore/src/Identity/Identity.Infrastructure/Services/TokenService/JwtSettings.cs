using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.TokenService.TokenService
{
    public record JwtSettings
    {
        public required string Secret { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
    }
}
