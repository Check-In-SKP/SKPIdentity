using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.TokenService
{
    public class RsaKeyManager
    {
        public RsaKeyManager(string privateKeyPath)
        {
            _privateKeyPath = _rsa.ToXmlString(true);
            _rsa = LoadOrCreateRsaKey();
        }

        
        public RSA PrivateKey => _rsa;
        private readonly RSA _rsa;
        public RsaSecurityKey PublicKey => new RsaSecurityKey(_rsa);

        private readonly string _privateKeyPath;

        private RSA LoadOrCreateRsaKey()
        {
            if (File.Exists(_privateKeyPath))
            {
                var rsaKey = RSA.Create();
                rsaKey.ImportRSAPrivateKey(File.ReadAllBytes(_privateKeyPath), out _);
                return rsaKey;
            }
            else
            {
                var rsaKey = RSA.Create();
                var privateKey = rsaKey.ExportRSAPrivateKey();
                File.WriteAllBytes(_privateKeyPath, privateKey);
                return rsaKey;
            }
        }
    }
}
