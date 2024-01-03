using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.ApiClientAggregate
{
    public class CustomRole : BaseEntity
    {
        private readonly Guid _id;
        public Guid Id => _id;
        public string Name { get; private set; }
        public string Description { get; private set; }

        // List of User IDs associated with this role (Bidirectional relationship)
        public List<Guid> UserIds { get; private set; }
    }
}
