using Identity.Domain.Common;
using Identity.Domain.Entities.ApiClientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class User : BaseEntity
    {
        private readonly Guid _id;
        public Guid Id => _id;

        public string Email { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public bool IsLocked { get; private set; }

        public string PasswordHash { get; private set; }

        // Defines a list of roles from the Role enum.
        // (used for authorization within the solution)
        public List<int> Roles { get; private set; }

        // Defines a list of custom roles defined from an ApiClient.
        // (used for authorization outside the scope of the solution)
        // (Bidirectional relationship)
        public List<Guid?> DynamicRoleIds { get; private set; }

        // List of Session IDs associated with a user.
        // (Bidirectional relationship)
        public List<Guid?> SessionIds { get; private set; }

        // List of ApiClient IDs associated with a user.
        // (Bidirectional relationship)
        public List<Guid?> ApiClientIds { get; private set; }
    }
}
