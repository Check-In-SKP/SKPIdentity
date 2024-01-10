using Identity.Application.Common.Services.Interfaces;
using Identity.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task DispatchEventsAsync(IEnumerable<BaseEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        public async Task DispatchEventAsync(BaseEvent domainEvent, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
