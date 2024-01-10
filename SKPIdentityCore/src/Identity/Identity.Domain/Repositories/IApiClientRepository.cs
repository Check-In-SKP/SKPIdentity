using Identity.Domain.Entities.ApiClientAggregate;

namespace Identity.Domain.Repositories
{
    public interface IApiClientRepository : IGenericRepository<ApiClient, Guid>
    {
    }
}
