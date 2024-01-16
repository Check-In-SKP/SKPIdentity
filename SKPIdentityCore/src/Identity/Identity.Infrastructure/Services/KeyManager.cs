using Identity.Infrastructure.Models;
using Identity.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Identity.Infrastructure.Services
{
    public class KeyManager : IKeyManager
    {
        private readonly string _keyPath;
        private readonly AsyncLazy<RSA> _rsaLazy;
        private readonly AsyncLazy<byte[]> _hmacLazy;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public KeyManager(string keyPath)
        {
            _keyPath = keyPath;
            _rsaLazy = new AsyncLazy<RSA>(() => LoadOrCreateKeyAsync<RSA>("RSAKey.json"));
            _hmacLazy = new AsyncLazy<byte[]>(() => LoadOrCreateKeyAsync<byte[]>("HMACKey.json"));
        }

        public Task<RSA> PrivateKeyAsync => _rsaLazy.Value;
        public Task<byte[]> HmacKeyAsync => _hmacLazy.Value;

        public async Task<RsaSecurityKey> GetPublicKeyAsync()
        {
            RSA rsaKey = await PrivateKeyAsync;
            return new RsaSecurityKey(rsaKey);
        }

        private async Task<T> LoadOrCreateKeyAsync<T>(string keyFileName)
        {
            await _semaphore.WaitAsync();
            try
            {
                string filePath = Path.Combine(_keyPath, keyFileName);
                if (File.Exists(filePath))
                {
                    string keyData = await File.ReadAllTextAsync(filePath);
                    var key = JsonConvert.DeserializeObject<T>(keyData);

                    return key == null ? throw new InvalidOperationException("Key data is null.") : key;
                }
                else
                {
                    // Generate a new key and save it to the JSON file
                    T newKey = GenerateRandomKey<T>();
                    string keyData = JsonConvert.SerializeObject(newKey);
                    await File.WriteAllTextAsync(filePath, keyData);
                    return newKey;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log them, or throw a custom exception.
                throw new Exception($"Failed to load or create {typeof(T).Name} key.", ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static T GenerateRandomKey<T>()
        {
            if (typeof(T) == typeof(byte[]))
            {
                // Generate a random byte array for HMAC key
                var hmacKey = new byte[64];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(hmacKey);
                }
                return (T)(object)hmacKey;
            }
            else if (typeof(T) == typeof(RSA))
            {
                // Generate a new RSA key pair
                using RSA rsa = RSA.Create();
                return (T)(object)rsa;
            }
            else
            {
                throw new NotSupportedException($"Key generation for type {typeof(T)} is not supported.");
            }
        }
    }
}
