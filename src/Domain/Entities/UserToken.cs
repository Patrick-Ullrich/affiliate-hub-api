using AffiliateHub.Domain.Common;

namespace AffiliateHub.Domain.Entities;
public class UserToken : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public User? User { get; set; }
}
