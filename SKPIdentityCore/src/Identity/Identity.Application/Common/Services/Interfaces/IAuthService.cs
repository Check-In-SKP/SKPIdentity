using Identity.SharedKernel.Models;

namespace Identity.Application.Common.Services.Interfaces
{
    public interface IAuthService
    {
        bool ValidateCodeVerifier(AuthCode code, string codeVerifier);
    }
}
