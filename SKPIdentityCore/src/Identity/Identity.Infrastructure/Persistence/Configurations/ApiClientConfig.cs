using Identity.Domain.Entities;
using Identity.Domain.Entities.ApiClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class ApiClientConfig : IEntityTypeConfiguration<ApiClient>
    {
        public void Configure(EntityTypeBuilder<ApiClient> builder)
        {
            //Primary Key
            builder.HasKey(ac => ac.Id);

            //Properties
            builder.Property(ac => ac.ApiKey)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(ac => ac.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Foreignkeys
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
