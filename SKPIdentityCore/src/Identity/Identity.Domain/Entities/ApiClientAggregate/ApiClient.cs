using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Common;

namespace Identity.Domain.Entities.ApiClientAggregate
{
    public class ApiClient : BaseEntity
    {
        public ApiClient(Guid id, string apiKey, string name, string? description, Guid userId)
        {
            _id = id;
            _apiKey = apiKey;
            _name = name;
            _description = description;
            _userId = userId;
        }
        
        public Guid Id => _id;
        private readonly Guid _id;

        public string ApiKey => _apiKey;
        private readonly string _apiKey;
        
        public string Name { get => _name; private set => _name = value; }
        private string _name;
        
        public string? Description { get => _description; private set => _description = value; }
        private string? _description;

        public Guid UserId => _userId;
        private readonly Guid _userId;
        
        public IReadOnlyCollection<DynamicRole?> DynamicRoles => _dynamicRoles.AsReadOnly();
        private readonly List<DynamicRole?> _dynamicRoles = new();
    }
}
