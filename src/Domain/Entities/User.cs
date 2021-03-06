using AffiliateHub.Domain.Common;

namespace AffiliateHub.Domain.Entities;
public class User : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IList<UserOneTimeCode> UserOneTimeCodes { get; private set; } = new List<UserOneTimeCode>();
    public IList<FileDetail> FileDetails { get; private set; } = new List<FileDetail>();
}
