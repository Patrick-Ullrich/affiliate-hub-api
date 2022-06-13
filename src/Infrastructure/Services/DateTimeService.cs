using AffiliateHub.Application.Common.Interfaces;

namespace AffiliateHub.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
