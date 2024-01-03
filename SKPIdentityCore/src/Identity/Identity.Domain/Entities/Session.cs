using Identity.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Session : BaseEntity
    {
        private readonly Guid _id;
        public Guid Id => _id;

        public string RefreshToken { get; private set; }

        public bool IsRevoked { get; private set; }

        public string IpAddress { get; private set; }

        public Guid DeviceId { get; private set; }

        public Guid UserId { get; private set; }
    }
}
