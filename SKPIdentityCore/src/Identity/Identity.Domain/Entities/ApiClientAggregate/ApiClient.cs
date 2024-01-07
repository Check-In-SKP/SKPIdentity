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
        public ApiClient(string apiKey, string projectName, List<DynamicRole?> dynamicRoles, Guid userId)
        {
            _id = Guid.NewGuid();
            ApiKey = apiKey;
            ProjectName = projectName;
            DynamicRoles = dynamicRoles;
            UserId = userId;
        }

        private readonly Guid _id;
        public Guid Id => _id;
        public string ApiKey { get; private set; }
        public string ProjectName { get; private set; }
        public List<DynamicRole?> DynamicRoles { get; private set; }
        public Guid UserId { get; private set; }
    }
}
