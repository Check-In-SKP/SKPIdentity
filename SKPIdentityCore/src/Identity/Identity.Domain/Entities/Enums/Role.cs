using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.Enums
{
    /// <summary> Static roles used in Identity </summary>
    public enum Role
    {
        Admin = 1,
        User = 2,
        ApiClient = 3,
    }
}
