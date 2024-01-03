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
        private readonly Guid _id;
        public Guid Id => _id;
        public string ApiKey { get; private set; }
        public string ProjectName { get; private set; }
        public List<CustomRole> CustomRoles { get; private set; }
        public Guid UserId { get; private set; }
    }
}
