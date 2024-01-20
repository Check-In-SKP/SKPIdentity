using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.SharedKernel.Models.Enums
{
    public enum TokenType
    {
        IdToken, // Used for authentication
        AccessToken, // Used for authorization
        NFCToken // Generated from AccessToken with a limited set of authorization access - Should have a very small lifespan (~1 min).
    }

    public static class TokenTypeExtensions
    {
        // Formats token type to fit standard naming conventions (used for external systems)
        public static string ToTokenString(this TokenType tokenType)
        {
            return tokenType switch
            {
                TokenType.IdToken => "id_token",
                TokenType.AccessToken => "access_token",
                TokenType.NFCToken => "nfc_token",
                _ => throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null)
            };
        }

        public static TokenType FromTokenStringToTokenType(this string tokenType)
        {
            return tokenType switch
            {
                "id_token" => TokenType.IdToken,
                "access_token" => TokenType.AccessToken,
                "nfc_token" => TokenType.NFCToken,
                _ => throw new ArgumentOutOfRangeException(nameof(tokenType), tokenType, null)
            };
        }
    }
}
