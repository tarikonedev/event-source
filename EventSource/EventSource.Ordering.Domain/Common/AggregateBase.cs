using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Domain.Common
{
    public abstract class AggregateBase<TId> : IAggregateRoot<TId>, IESAggregateRoot<TId>
    {

        public const long NewAggregateVersion = 0;

        private long _version = NewAggregateVersion;

        private IList<IDomainEvent<TId>> _uncommittedEvents = new List<IDomainEvent<TId>>();

        public TId Id { get; protected set; }

        public long Version => _version;

        public void ApplyEvent(IDomainEvent<TId> @event, long version)
        {
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public IEnumerable<IDomainEvent<TId>> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        protected void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : DomainEventBase<TId>
        {
            IDomainEvent<TId> eventWithAggregate = @event.WithAggregate(Equals(Id, default(TId)) ? @event.AggregateId : Id, _version);

            ((IESAggregateRoot<TId>)this).ApplyEvent(eventWithAggregate, _version + 1);

            _uncommittedEvents.Add(eventWithAggregate);
        }
    }
}
