using AffiliateHub.Domain.Common;

namespace AffiliateHub.Domain.Entities;

public enum FileType
{
    Image
}

public class FileDetail : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string BlurHash { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public FileType Type { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

}