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

        // EF Core constructor
        private ApiClient() { }
        
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
