using AffiliateHub.Domain.Common;

namespace AffiliateHub.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
