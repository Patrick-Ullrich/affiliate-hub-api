using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Services.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AffiliateHub.Application.Users.Commands.LoginUser;

public record LoginUserCommand : IRequest<AuthData>
{
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthData>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly ILogger<LoginUserCommandHandler> _logger;
    
    public LoginUserCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper, ILogger<LoginUserCommandHandler> logger)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthData> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == request.EmailAddress, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Email address or password is incorrect.");
        }

        var isPasswordValid = _authService.VerifyPassword(request.Password, user.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Email address or password is incorrect.");
        }

        return _authService.GetAuthData(_mapper.Map(user, new UserDto()));
    }
}
