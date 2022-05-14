namespace AffiliateHub.Application.Users.Dto
{
    public class AuthData
    {
        public string Token { get; set; } = string.Empty;
        public long TokenExpirationTime { get; set; }
    }
}