using AffiliateHub.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AffiliateHub.Infrastructure.Services;

public class EmailService : IEmailService
{

    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IEnvironment _env;

    public EmailService(ILogger<EmailService> logger, IConfiguration configuration, IEnvironment env)
    {
        _logger = logger;
        _configuration = configuration;
        _env = env;
    }


    public async Task SendEmailAsync(string to, string toName, string subject, string body)
    {
        _logger.LogInformation("Sending email to {to} with subject {subject}", to, subject);

        if (!_env.IsDevelopment())
        {
            var client = new SendGridClient(_configuration.GetValue<string>("Email:SendGridApiKey"));
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configuration.GetValue<string>("Email:From"), _configuration.GetValue<string>("Email:FromName")),
                Subject = subject,
                HtmlContent = body
            };
            msg.AddTo(new EmailAddress(to, toName));

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation("Email sent to {to} with subject {subject}", to, subject);
            }
            else
            {
                _logger.LogError("Error sending email to {to} with subject {subject}", to, subject);
            }
        }
        else
        {
            _logger.LogInformation("Environment is Development, email not send.");
        }
    }
}
