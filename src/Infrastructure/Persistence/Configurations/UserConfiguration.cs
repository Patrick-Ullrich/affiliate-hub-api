using AffiliateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AffiliateHub.Infrastructure.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(t => t.FirstName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.EmailAddress)
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(t => t.PhoneNumber)
            .HasMaxLength(64);

        builder.Property(t => t.Password)
            .IsRequired();

        builder.HasIndex(t => t.EmailAddress)
            .IsUnique();
    }
}
