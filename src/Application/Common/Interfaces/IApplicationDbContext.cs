using AffiliateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AffiliateHub.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<UserToken> UserTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}