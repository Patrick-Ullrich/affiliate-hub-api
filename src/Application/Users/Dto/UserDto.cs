using AffiliateHub.Application.Common.Mappings;
using AffiliateHub.Domain.Entities;

namespace AffiliateHub.Application.Users.Dto
{
    public class UserDto : IMapFrom<User>
    {
        public string Id { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}