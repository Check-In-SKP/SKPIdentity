using Identity.Domain.Common;

namespace Identity.Domain.Entities
{
    public class Session : BaseEntity
    {
        private readonly Guid _id;
        private string _refreshToken;
        private bool _isRevoked;
        private string _ipAddress;
        private readonly string _userAgent;
        private readonly Guid _userId;

        public Session(Guid id, string refreshToken, string ipAddress, string userAgent, Guid userId)
        {
            _id = id;
            _refreshToken = refreshToken;
            _isRevoked = false;
            _ipAddress = ipAddress;
            _userAgent = userAgent;
            _userId = userId;
        }

        // EF Core constructor
        private Session() { }

        public Guid Id => _id;
        public string RefreshToken { get => _refreshToken; private set => _refreshToken = value; }
        public bool IsRevoked { get => _isRevoked; private set => _isRevoked = value; }
        public string IpAddress { get => _ipAddress; private set=> _ipAddress = value; }
        public string UserAgent => _userAgent;
        public Guid UserId => _userId;

        // Session methods
        public void Revoke()
        {
            IsRevoked = true;
        }

        public void UpdateIpAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}