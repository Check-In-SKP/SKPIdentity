using Identity.Domain.Entities.ApiClientAggregate;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;

namespace Identity.Infrastructure.Repositories
{
    public class DynamicRoleRepository : GenericRepository<DynamicRole, Guid>, IDynamicRoleRepository
    {
        public DynamicRoleRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
