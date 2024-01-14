using Identity.Domain.Entities;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        //public void RemoveDynamicRoleFromUser(Guid userId, Guid dynamicRoleId);
        //public void AddDynamicRoleToUser(Guid userId, Guid dynamicRoleId);
    }
}
