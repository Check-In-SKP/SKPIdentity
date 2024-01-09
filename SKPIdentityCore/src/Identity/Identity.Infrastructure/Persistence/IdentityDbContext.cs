using Microsoft.EntityFrameworkCore;
using Identity.Domain.Entities.ApiClientAggregate;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext
    {
        // Commands for migrations:
        // dotnet ef migrations add IdentityDB --project src/Identity/Identity.Infrastructure --startup-project src/Identity/Identity.API --output-dir Data/Migrations
        // dotnet ef database update IdentityDB --project src/Identity/Identity.Infrastructure --startup-project src/Identity/Identity.API

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
