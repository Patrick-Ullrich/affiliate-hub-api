using AffiliateHub.Application.Users.Dtos;
using AffiliateHub.Application.Users.Queries.GetUser;

namespace AffiliateHub.Application.Users.Services.Interfaces
{
    public interface IAuthService
    {
        AuthData GetAuthData(UserDto user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}
