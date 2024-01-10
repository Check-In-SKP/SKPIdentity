using Identity.Domain.Entities;
using Identity.Domain.Entities.ApiClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.IsEmailConfirmed)
                .IsRequired();

            builder.Property(i => i.PasswordHash)
                .IsRequired();

            builder.Property(u => u.IsLocked)
                .IsRequired();

            // DynamicRoleIds as foreign keys to DynamicRoles
            builder.HasMany<DynamicRole>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                "UserDynamicRole", // Join table name
                j => j.HasOne<DynamicRole>().WithMany().HasForeignKey("DynamicRoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));

            // SessionIds as foreign keys to ApiClients
            builder.HasMany<Session>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                "UserSession", // Join table name
                j => j.HasOne<Session>().WithMany().HasForeignKey("SessionId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));

            // ApiClientIds as foreign keys to ApiClients
            builder.HasMany<ApiClient>()
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                "UserApiClient", // Join table name
                j => j.HasOne<ApiClient>().WithMany().HasForeignKey("ApiClientId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));
        }
    }
}
