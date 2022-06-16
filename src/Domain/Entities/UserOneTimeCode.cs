using AffiliateHub.Domain.Common;

namespace AffiliateHub.Domain.Entities;

public enum UserOneTimeCodeType
{
    PasswordReset
}

public class UserOneTimeCode : AuditableEntity, IHasDomainEvent
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public UserOneTimeCodeType Type { get; set; }
    public DateTime ExpiresAt { get; set; }
    public User? User { get; set; }
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}


