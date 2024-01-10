using Identity.Domain.Entities.ApiClientAggregate;

namespace Identity.Domain.Repositories
{
    public interface IDynamicRoleRepository : IGenericRepository<DynamicRole, Guid>
    {
    }
}
