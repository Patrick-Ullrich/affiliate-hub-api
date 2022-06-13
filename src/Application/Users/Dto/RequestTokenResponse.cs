namespace AffiliateHub.Application.Users.Dto
{
    public class RequestTokenResponse
    {
        // We only use this in Dev, where we return the token in the response.
        // Instead of sending the token via email.
        public string Token { get; set; } = string.Empty;
    }
}