using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Services.Interfaces;
using AffiliateHub.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AffiliateHub.Application.Users.Commands.RequestPasswordReset;

public record RequestPasswordResetCommand : IRequest<RequestTokenResponse>
{
    public string EmailAddress { get; set; } = string.Empty;
}

public class RequestPasswordResetCommandHandler : IRequestHandler<RequestPasswordResetCommand, RequestTokenResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ILogger _logger;
    private readonly IEnvironment _environment;
    private readonly IEmailService _emailService;

    public RequestPasswordResetCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper,
        ILogger<RequestPasswordResetCommandHandler> logger, IEnvironment environment, IEmailService emailService)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
        _logger = logger;
        _environment = environment;
        _emailService = emailService;
    }

    public async Task<RequestTokenResponse> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == request.EmailAddress, cancellationToken);

        if (user == null)
        {
            _logger.LogInformation("Tried to reset password using email address {EmailAddress} does not exist.", request.EmailAddress);
            // We don't throw an error 
            return new RequestTokenResponse
            {
                Token = string.Empty
            };
        }

        // Check if we already have a request for this user and delete it if so
        var existingUserOneTimeCode = await _context.UserOneTimeCodes.SingleOrDefaultAsync(ut => ut.UserId == user.Id && ut.Type == UserOneTimeCodeType.PasswordReset, cancellationToken);

        if (existingUserOneTimeCode != null)
        {
            _context.UserOneTimeCodes.Remove(existingUserOneTimeCode);
        }

        var userOneTimeCode = new UserOneTimeCode
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            Type = UserOneTimeCodeType.PasswordReset,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };

        _context.UserOneTimeCodes.Add(userOneTimeCode);

        await _context.SaveChangesAsync(cancellationToken);

        await _emailService.SendEmailAsync(user.EmailAddress, user.FirstName, "Reset Password",
            $"<p>Hi {user.FirstName}, here is your code {userOneTimeCode.Token}</p>");

        if (_environment.IsDevelopment())
        {
            return new RequestTokenResponse
            {
                Token = userOneTimeCode.Token
            };
        }

        return new RequestTokenResponse
        {
            Token = string.Empty
        };
    }
}
