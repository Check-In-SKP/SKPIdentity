using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.SharedKernel.Models
{
    public class AuthCode
    {
        public string ClientId { get; set; }  // Identifier for the client making the request
        public string CodeChallenge { get; set; }  // PKCE code challenge
        public string CodeChallengeMethod { get; set; }  // PKCE code challenge method (e.g., S256)
        public string RedirectUri { get; set; }  // URI to redirect after authorization
        public string Scope { get; set; }  // Requested scope values for the access token
        public string State { get; set; }  // Opaque value used to maintain state between the request and callback
        public string Subject { get; set; }  // Identifier for the resource owner who granted authorization
        public DateTime Expiry { get; set; }  // Expiry time of the code
    }
}
