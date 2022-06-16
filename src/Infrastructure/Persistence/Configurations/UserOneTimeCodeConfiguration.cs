using AffiliateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AffiliateHub.Infrastructure.Persistence.Configurations;

public class UserOneTimeCodeConfiguration : IEntityTypeConfiguration<UserOneTimeCode>
{
    public void Configure(EntityTypeBuilder<UserOneTimeCode> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.ExpiresAt)
            .IsRequired();

        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.Type)
            .HasConversion<string>();
    }
}
