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

        public List<DynamicRole?> DynamicRoles { get => _dynamicRoles; private set => _dynamicRoles = value; }
        private List<DynamicRole?> _dynamicRoles = new();

        public Guid UserId => _userId;
        private readonly Guid _userId;

        public void AddDynamicRole(DynamicRole dynamicRole)
        {
            DynamicRoles.Add(dynamicRole);
        }

        public void RemoveDynamicRole(DynamicRole dynamicRole)
        {
            DynamicRoles.Remove(dynamicRole);
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }
    }
}
