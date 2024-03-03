using Identity.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _workFactor = 10; // Number times to rehash password

        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
