using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities.ApiClientAggregate;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
    {
        public DbSet<ApiClient> ApiClients { get; set; }
        public DbSet<DynamicRole> DynamicRoles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
