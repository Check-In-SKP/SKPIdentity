using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class SessionConfig : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Primary key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.RefreshToken)
                .IsRequired();

            builder.Property(s => s.IpAddress)
                .IsRequired();

            builder.Property(s => s.DeviceId)
                .IsRequired();

            builder.Property(s => s.IsRevoked)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
