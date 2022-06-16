
namespace AffiliateHub.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string toName, string subject, string body);
}