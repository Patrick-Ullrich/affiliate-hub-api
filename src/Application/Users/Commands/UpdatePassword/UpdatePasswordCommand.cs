using AffiliateHub.Application.Common.Exceptions;
using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Services.Interfaces;
using AffiliateHub.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AffiliateHub.Application.Users.Commands.UpdatePassword;

public record UpdatePasswordCommand : IRequest<UserDto>
{
    public string Password { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
}

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePasswordCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<UserDto> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == _currentUserService.UserId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), _currentUserService.UserId ?? "");
        }

        if (!_authService.VerifyPassword(request.CurrentPassword, user.Password))
        {
            throw new ValidationException(nameof(request.CurrentPassword), $"Current Password is incorrect");
        }

        user.Password = _authService.HashPassword(request.Password);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map(user, new UserDto());
    }
}
