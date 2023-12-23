using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities.UserAggregate;

namespace Identity.Domain.Entities
{
    public class Session
    {
        Guid Id { get; set; }
        User User { get; set; }
        string JWTToken { get; set; }
        DateTime ExpirationDate { get; set; }
        bool IsActive { get; set; }
        bool IsRevoked { get; set; }
    }
}
