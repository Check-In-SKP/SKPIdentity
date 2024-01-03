using Identity.Domain.Entities.ApiClientAggregate;
using Identity.Infrastructure.Persistence;
using Identity.Domain.Repositories;

namespace Identity.Infrastructure.Repositories
{
    public class ApiClientRepository : GenericRepository<ApiClient, Guid>, IApiClientRepository
    {
        public ApiClientRepository(IdentityDbContext context) : base(context)
        {
        }
    }
}
