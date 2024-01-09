using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Common
{
    public class BaseEntity : BaseAuditable
    {
        private readonly List<BaseEvent> _domainEvents = new();
        
        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(BaseEvent eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
