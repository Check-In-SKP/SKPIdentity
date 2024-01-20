using Identity.Domain.Common;
using Identity.Domain.Entities.Enums;

namespace Identity.Domain.Entities
{
    public class User : BaseEntity
    {
        private readonly Guid _id;
        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private bool _isEmailConfirmed;
        private bool _isLocked;
        private string _passwordHash;
        private List<int> _roles;
        private List<Guid?> _dynamicRoleIds;
        private List<Guid?> _sessionIds;
        private List<Guid?> _apiClientIds;

        public User(Guid id, string username, string firstName, string lastName, string email, bool isEmailConfirmed, bool isLocked, string passwordHash, List<int> roles)
        {
            _id = id;
            _username = username;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _isEmailConfirmed = isEmailConfirmed;
            _isLocked = isLocked;
            _passwordHash = passwordHash;
            _roles = roles;
        }

        // EF Core constructor
        private User() { }

        public Guid Id => _id;
        public string Username { get => _username; private set => _username = value; }
        public string FirstName { get => _firstName; private set => _firstName = value; }
        public string LastName { get => _firstName; private set=> _firstName = value; }
        public string Email { get => _firstName; private set => _firstName = value; }
        public bool IsEmailConfirmed { get => _isEmailConfirmed; private set => _isEmailConfirmed = value; }
        public bool IsLocked { get => _isLocked; private set => _isLocked = value; }
        public string PasswordHash { get => _firstName; private set => _firstName = value; }
        public List<int> Roles { get => _roles; private set => _roles = value; }
        public List<Guid?> DynamicRoleIds { get => _dynamicRoleIds; private set => _dynamicRoleIds = value; } // Bidirectional relationship
        public List<Guid?> SessionIds { get => _sessionIds; private set => _sessionIds = value; } // Bidirectional relationship
        public List<Guid?> ApiClientIds { get => _apiClientIds; private set => _apiClientIds = value; } // Bidirectional relationship

        // User methods
        public void UpdateUsername(string username)
        {
            Username = username;
        }

        public void UpdateFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void UpdateLastName(string lastName)
        {
            LastName = lastName;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void EmailConfirmed()
        {
            if (!IsEmailConfirmed)
                IsEmailConfirmed = true;
        }

        public void Lock()
        {
            if (!IsLocked)
                IsLocked = true;
        }

        public void Unlock()
        {
            if (IsLocked)
                IsLocked = false;
        }

        public void UpdatePasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void AddRole(int role)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
        }

        public void RemoveRole(int role)
        {
            Roles.Remove(role);
        }

        public void ClearRoles()
        {
            Roles.Clear();
        }

        // Implement event for bidirectional relationship
        public void AddDynamicRoleId(Guid? dynamicRoleId)
        {
            if (!DynamicRoleIds.Contains(dynamicRoleId))
                DynamicRoleIds.Add(dynamicRoleId);
        }

        // Implement event for bidirectional relationship
        public void RemoveDynamicRoleId(Guid? dynamicRoleId)
        {
            DynamicRoleIds.Remove(dynamicRoleId);
        }

        // Implement event for bidirectional relationship
        public void ClearDynamicRoleIds()
        {
            DynamicRoleIds.Clear();
        }

        // Implement event for bidirectional relationship
        public void AddSessionId(Guid? sessionId)
        {
            if (!SessionIds.Contains(sessionId))
                SessionIds.Add(sessionId);
        }

        // Implement event for bidirectional relationship
        public void RemoveSessionId(Guid? sessionId)
        {
            SessionIds.Remove(sessionId);
        }

        // Implement event for bidirectional relationship
        public void ClearSessionIds()
        {
            SessionIds.Clear();
        }

        // Implement event for bidirectional relationship
        public void AddApiClientId(Guid? apiClientId)
        {
            if (!ApiClientIds.Contains(apiClientId))
                ApiClientIds.Add(apiClientId);
        }

        // Implement event for bidirectional relationship
        public void RemoveApiClientId(Guid? apiClientId)
        {
            ApiClientIds.Remove(apiClientId);
        }

        // Implement event for bidirectional relationship
        public void ClearApiClientIds()
        {
            ApiClientIds.Clear();
        }
    }
}
