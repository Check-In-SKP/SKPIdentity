using Identity.Domain.Common;

namespace Identity.Application.Common.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync(IEnumerable<BaseEvent> domainEvents, CancellationToken cancellationToken = default);
        Task DispatchEventAsync(BaseEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
