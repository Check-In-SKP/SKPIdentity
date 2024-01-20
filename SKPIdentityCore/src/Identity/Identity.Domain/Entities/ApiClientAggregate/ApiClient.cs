using Identity.Domain.Common;

namespace Identity.Domain.Entities.ApiClientAggregate
{
    public class ApiClient : BaseEntity
    {
        private readonly Guid _id;
        private readonly string _apiKey;
        private string _name;
        private string? _description;
        private List<DynamicRole?> _dynamicRoles = new();
        private readonly Guid _userId;

        public ApiClient(Guid id, string apiKey, string name, string? description, Guid userId)
        {
            _id = id;
            _apiKey = apiKey;
            _name = name;
            _description = description;
            _userId = userId;
        }

        // EF Core constructor
        private ApiClient() { }
        
        public Guid Id => _id;
        public string ApiKey => _apiKey;
        public string Name { get => _name; private set => _name = value; }
        public string? Description { get => _description; private set => _description = value; }
        public List<DynamicRole?> DynamicRoles { get => _dynamicRoles; private set => _dynamicRoles = value; }
        public Guid UserId => _userId;
        
        // ApiClient methods
        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }

        public void AddDynamicRole(DynamicRole dynamicRole)
        {
            DynamicRoles.Add(dynamicRole);
        }

        public void RemoveDynamicRole(DynamicRole dynamicRole)
        {
            DynamicRoles.Remove(dynamicRole);
        }
        
        public void ClearDynamicRoles()
        {
            DynamicRoles.Clear();
        }

        // DynamicRole methods
        public void UpdateDynamicRoleName(Guid dynamicRoleId, string name)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.UpdateName(name);
        }

        public void UpdateDynamicRoleDescription(Guid dynamicRoleId, string description)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.UpdateDescription(description);
        }

        public void AddDynamicRoleUser(Guid dynamicRoleId, Guid userId)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.AddUser(userId);
        }

        public void RemoveDynamicRoleUser(Guid dynamicRoleId, Guid userId)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.RemoveUser(userId);
        }

        public void ClearDynamicRoleUsers(Guid dynamicRoleId)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.ClearUsers();
        }

        public void AddDynamicRoleUsers(Guid dynamicRoleId, List<Guid?> userIds)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.AddUsers(userIds);
        }

        public void RemoveDynamicRoleUsers(Guid dynamicRoleId, List<Guid?> userIds)
        {
            var dynamicRole = DynamicRoles.FirstOrDefault(dr => dr?.Id == dynamicRoleId);
            if (dynamicRole is null) return;
            dynamicRole.RemoveUsers(userIds);
        }
    }
}
