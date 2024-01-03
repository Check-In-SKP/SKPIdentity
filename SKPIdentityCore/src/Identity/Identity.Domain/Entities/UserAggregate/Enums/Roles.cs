using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<<< HEAD:SKPIdentityCore/src/Identity/Identity.Domain/Entities/Enums/Role.cs
namespace Identity.Domain.Entities.Enums
========
namespace Identity.Domain.Entities.UserAggregate.Enums
>>>>>>>> 75127a7de546642be488afb46c14012df7fe8412:SKPIdentityCore/src/Identity/Identity.Domain/Entities/UserAggregate/Enums/Roles.cs
{
    /// <summary> Static roles used in Identity </summary>
    public enum Role
    {
        Admin = 1,
        User = 2,
        ApiClient = 3,
    }
}
