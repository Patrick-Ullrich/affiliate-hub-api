using AffiliateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AffiliateHub.Infrastructure.Persistence.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(t => t.ExpiresAt)
            .IsRequired();

        builder.Property(t => t.UserId)
            .IsRequired();
    }
}
