using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class AuditableEntity : DomainEntity
    {
        public string? CreatedBy { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
    }
}
