using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Interfaces
{
    public interface IKeyManager
    {
        Task<RsaSecurityKey> GetPrivateRsaKeyAsync();
        Task<RsaSecurityKey> GetPublicRsaKeyAsync();
        Task<RSA> RsaKeyPairAsync {  get; }
        Task<byte[]> HmacKeyAsync {  get; }
    }
}
