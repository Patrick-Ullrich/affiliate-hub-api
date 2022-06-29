using AffiliateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AffiliateHub.Infrastructure.Persistence.Configurations;

public class FileDetailsConfiguration : IEntityTypeConfiguration<FileDetail>
{
    public void Configure(EntityTypeBuilder<FileDetail> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.FilePath)
        .IsRequired();

        builder.HasIndex(t => new { t.FilePath, t.BucketName })
            .IsUnique();
    }
}
