using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities.ApiClientAggregate
{
    public class DynamicRole : BaseEntity
    {
        public DynamicRole(Guid id, string name, string? description, Guid apiClientId, List<Guid> userIds)
        {
            _id = id;
            _name = name;
            _description = description;
            _apiClientId = apiClientId;
            _userIds = userIds;
        }

        public Guid Id => _id;
        private readonly Guid _id;

        public string Name { get => _name; private set => _name = value; }
        private string _name;
        
        public string? Description { get => _description; private set => _description = value; }
        private string? _description;

        // ApiClient ID associated with this role
        public Guid ApiClientId => _apiClientId;
        private readonly Guid _apiClientId;

        // List of User IDs associated with this role (Bidirectional relationship)
        public List<Guid> UserIds { get => _userIds; private set => _userIds = value; }
        private List<Guid> _userIds;
    }
}
    