using AffiliateHub.Application.Common.Interfaces;
using AffiliateHub.Application.Users.Dto;
using AffiliateHub.Application.Users.Services.Interfaces;
using AffiliateHub.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AffiliateHub.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<AuthData>
{
    public string EmailAddress { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthData>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<AuthData> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            EmailAddress = request.EmailAddress,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Password = _authService.HashPassword(request.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return _authService.GetAuthData(_mapper.Map(user, new UserDto()));
    }
}
