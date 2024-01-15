using Identity.Application.Common.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;

namespace Identity.Infrastructure.Services.OAuthService
{
    public class DataProtectorService : IDataProtectorService
    {
        private readonly IDataProtector _protector;

        public DataProtectorService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("Identity_DataProtector");
        }

        public string Protect(string input) => input == null ? throw new ArgumentNullException(nameof(input)) : _protector.Protect(input);
        public string Unprotect(string protectedInput) => protectedInput == null ? throw new ArgumentNullException(nameof(protectedInput)) : _protector.Unprotect(protectedInput);
    }
}
