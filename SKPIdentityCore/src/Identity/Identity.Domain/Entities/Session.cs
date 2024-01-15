using Identity.Domain.Common;

namespace Identity.Domain.Entities
{
    public class Session : BaseEntity
    {
        public Session(Guid id, string refreshToken, string ipAddress, Guid deviceId, string userAgent, Guid userId)
        {
            _id = id;
            _refreshToken = refreshToken;
            _isRevoked = false;
            _ipAddress = ipAddress;
            _deviceId = deviceId;
            _userAgent = userAgent;
            _userId = userId;
        }

        // EF Core constructor
        private Session() { }

        public Guid Id => _id;
        private readonly Guid _id;

        public string RefreshToken { get => _refreshToken; private set => _refreshToken = value; }
        private string _refreshToken;

        public bool IsRevoked { get => _isRevoked; private set => _isRevoked = value; }
        private bool _isRevoked;

        public string IpAddress { get => _ipAddress; private set=> _ipAddress = value; }
        private string _ipAddress;

        public Guid DeviceId => _deviceId;
        private readonly Guid _deviceId;

        public string UserAgent => _userAgent;
        private readonly string _userAgent;

        public Guid UserId => _userId;
        private readonly Guid _userId;

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