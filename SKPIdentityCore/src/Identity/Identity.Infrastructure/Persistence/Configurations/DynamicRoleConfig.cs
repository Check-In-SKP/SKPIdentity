using Identity.Domain.Entities.ApiClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class DynamicRoleConfig : IEntityTypeConfiguration<DynamicRole>
    {
        public void Configure(EntityTypeBuilder<DynamicRole> builder)
        {

        }
    }
}
