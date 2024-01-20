using Identity.Domain.Common;

namespace Identity.Domain.Entities.ApiClientAggregate
{
    public class DynamicRole : BaseEntity
    {
        private readonly Guid _id;
        private string _name;
        private string? _description;
        private List<Guid?> _userIds = new();
        
        internal DynamicRole(Guid id, string name, string? description)
        {
            _id = id;
            _name = name;
            _description = description;
        }

        // EF Core constructor
        private DynamicRole() { }

        public Guid Id => _id;
        public string Name { get => _name; private set => _name = value; }
        public string? Description { get => _description; private set => _description = value; }
        public List<Guid?> UserIds { get => _userIds; private set => _userIds = value; } // Bidirectional relationship

        internal void UpdateName(string name)
        {
            Name = name;
        }

        internal void UpdateDescription(string description)
        {
            Description = description;
        }

        // Implement event for bidirectional relationship
        internal void AddUser(Guid userId)
        {
            if (!UserIds.Contains(userId))
                UserIds.Add(userId);
        }

        // Implement event for bidirectional relationship
        internal void RemoveUser(Guid userId)
        {
            UserIds.Remove(userId);
        }

        // Implement event for bidirectional relationship
        internal void ClearUsers()
        {
            UserIds.Clear();
        }

        // Implement event for bidirectional relationship
        internal void AddUsers(List<Guid?> userIds)
        {
            UserIds.AddRange(userIds);
        }

        // Implement event for bidirectional relationship
        internal void RemoveUsers(List<Guid?> userIds)
        {
            UserIds.RemoveAll(x => userIds.Contains(x));
        }
    }
}
    