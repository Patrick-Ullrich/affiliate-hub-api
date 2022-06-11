using System.Security.Claims;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AffiliateHub.Application.Users.Services;

public class AuthService : IAuthService
{
    string jwtSecret;
    int jwtLifespan;
    string issuer;
    string audience;
    public AuthService(string jwtSecret, int jwtLifespan, string issuer, string audience)
    {
        this.jwtSecret = jwtSecret;
        this.jwtLifespan = jwtLifespan;
        this.issuer = issuer;
        this.audience = audience;
    }
    public AuthData GetAuthData(UserDto user)
    {
        var expirationTime = DateTime.UtcNow.AddSeconds(jwtLifespan);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
            Issuer = this.issuer,
            Audience = this.audience,
            Expires = expirationTime,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        return new AuthData
        {
            Token = token,
            TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
        };
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}