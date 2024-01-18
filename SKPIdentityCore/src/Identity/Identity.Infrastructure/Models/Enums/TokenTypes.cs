using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Models.Enums
{
    public enum TokenTypes
    {
        IdToken, // Used for authentication
        Accesstoken, // Used for authorization
        NFCToken // Generated from AccessToken with a limited set of authorization access - Should have a very small lifespan (~1 min).
    }
}
