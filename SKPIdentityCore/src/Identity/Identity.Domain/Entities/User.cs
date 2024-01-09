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
        public User(Guid id, string username, string firstName, string lastName, string email, bool isEmailConfirmed, bool isLocked, string passwordHash, List<int> roles, List<Guid?> dynamicRoleIds, List<Guid?> sessionIds, List<Guid?> apiClientIds)
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
            _dynamicRoleIds = dynamicRoleIds;
            _sessionIds = sessionIds;
            _apiClientIds = apiClientIds;
        }

        public Guid Id => _id;
        private readonly Guid _id;

        public string Username { get => _username; private set => _username = value; }
        private string _username;

        public string FirstName { get => _firstName; private set => _firstName = value; }
        private string _firstName;

        public string LastName { get => _firstName; private set=> _firstName = value; }
        private string _lastName;

        public string Email { get => _firstName; private set => _firstName = value; }
        private string _email;

        public bool IsEmailConfirmed { get => _isEmailConfirmed; private set => _isEmailConfirmed = value; }
        private bool _isEmailConfirmed;

        public bool IsLocked { get => _isLocked; private set => _isLocked = value; }
        private bool _isLocked;

        public string PasswordHash { get => _firstName; private set => _firstName = value; }
        private string _passwordHash;

        // Defines a list of roles from the Role enum.
        // (used for authorization within the solution)
        public List<int> Roles { get => _roles; private set => _roles = value; }
        private List<int> _roles;

        // Defines a list of custom roles defined from an ApiClient.
        // (used for authorization roles outside of Identity scope)
        // (Bidirectional relationship)
        public List<Guid?> DynamicRoleIds { get => _dynamicRoleIds; private set => _dynamicRoleIds = value; }
        private List<Guid?> _dynamicRoleIds;

        // List of Session IDs associated with a user.
        // (Bidirectional relationship)
        public List<Guid?> SessionIds { get => _sessionIds; private set => _sessionIds = value; }
        private List<Guid?> _sessionIds;

        // List of ApiClient IDs associated with a user.
        // (Bidirectional relationship)
        public List<Guid?> ApiClientIds { get => _apiClientIds; private set => _apiClientIds = value; }
        private List<Guid?> _apiClientIds;
    }
}
