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

    public RequestPasswordResetCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper, ILogger<RequestPasswordResetCommandHandler> logger)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<RequestTokenResponse> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == request.EmailAddress, cancellationToken);

        if (user == null)
        {
            _logger.LogInformation("Tried to reset password using email address {EmailAddress} does not exist.", request.EmailAddress);
        }
        else
        {
            // Check if we already have a request for this user and delete it if so
            var existingUserToken = await _context.UserTokens.SingleOrDefaultAsync(ut => ut.UserId == user.Id, cancellationToken);

            if (existingUserToken != null)
            {
                _context.UserTokens.Remove(existingUserToken);
            }

            var token = Guid.NewGuid().ToString();
            var userToken = new UserToken
            {
                Id = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new RequestTokenResponse
            {
                Token = token
            };
        }

        return new RequestTokenResponse
        {
            Token = string.Empty
        };
    }
}
