using Identity.Domain.Entities.ApiClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class DynamicRoleConfig : IEntityTypeConfiguration<DynamicRole>
    {
        public void Configure(EntityTypeBuilder<DynamicRole> builder)
        {
            //Primary Key
            builder.HasKey(dr => dr.Id);

            //Properties
            builder.Property(dr => dr.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(dr => dr.Description)
                .IsRequired()
                .HasMaxLength(256);

            // ApiClientId as foreign key to ApiClients
            builder.HasOne<ApiClient>()
                .WithMany()
                .HasForeignKey(dr => dr.ApiClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
