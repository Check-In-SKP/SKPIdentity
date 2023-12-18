using Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.UserAggregate
{
    public class User
    {
        private readonly Guid _id;
        public Guid Id => _id;

        public string Email { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public bool IsLocked { get; private set; }

        public string PasswordHash { get; private set; }

        public List<Roles> Roles { get; private set; }

        public List<DynamicRole>? DynamicRoles { get; private set; }
    }
}
