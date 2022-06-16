using AffiliateHub.Application.Common.Exceptions;
using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Application.Users.Services.Interfaces;
using AffiliateHub.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AffiliateHub.Application.Users.Commands.PasswordReset;

public record PasswordResetCommand : IRequest
{
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ILogger _logger;

    public PasswordResetCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper, ILogger<PasswordResetCommandHandler> logger)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        var userOneTimeCode = await _context.UserOneTimeCodes.SingleOrDefaultAsync(u => u.Token == request.Token, cancellationToken);

        if (userOneTimeCode == null)
        {
            throw new ValidationException(nameof(request.Token), "Token invalid or expired, please request a new token.");
        }

        if (userOneTimeCode.ExpiresAt < DateTime.UtcNow)
        {
            throw new ValidationException(nameof(request.Token), "Token invalid or expired, please request a new token.");
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userOneTimeCode.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), userOneTimeCode.UserId);
        }

        user.Password = _authService.HashPassword(request.Password);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
