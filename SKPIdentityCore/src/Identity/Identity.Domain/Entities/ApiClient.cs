using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Common;
using Identity.Domain.Entities.UserAggregate;

namespace Identity.Domain.Entities
{
    public class ApiClient : BaseEntity
    {
        public Guid Id { get; set; }
        public string ApiKey { get; set; }
        public string ProjectName { get; set; }
        public User User { get; set; }
    }
}
