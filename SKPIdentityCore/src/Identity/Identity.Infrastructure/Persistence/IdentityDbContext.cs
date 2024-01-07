using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities.ApiClientAggregate;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        public DbSet<ApiClient> ApiClients { get; set; }
        public DbSet<DynamicRole> DynamicRoles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
        }
    }
}
