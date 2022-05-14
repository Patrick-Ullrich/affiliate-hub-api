using AutoMapper;
using AffiliateHub.Application.Common.Interfaces;
using MediatR;
using AutoMapper.QueryableExtensions;
using AffiliateHub.Application.Common.Exceptions;
using AffiliateHub.Domain.Entities;
using AffiliateHub.Application.Users.Dto;

namespace AffiliateHub.Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<UserDto>
{
    public string Id { get; set; } = string.Empty;
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return _mapper.Map(user, new UserDto());
    }
}