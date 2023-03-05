using EventSource.Ordering.Application.Common.EventPublisher;
using EventSource.Ordering.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Infrastructure.Persistance
{
    public class InMemoryDomainEventPublisher<TAggregateId> : IDomainEventPublisher<TAggregateId>
    {
        private Subject subject;
        private Observer observer;

        public InMemoryDomainEventPublisher()
        {
            subject = new Subject();
            observer = new Observer(new InMemoryReadRepository(), subject);
        }

        public async Task PublishEvent(string eventType, string aggregateId, IDictionary<string, object> eventData)
        {
            InMemoryReadPersistance.PublishedEvents.Add(new MemoryPersistanceDomainEventModel()
            {
                EventType = eventType,
                EventDate = DateTime.Now,
                AggregateId = aggregateId,
                EventData = eventData
            });

            //SimpleLogger.Log(eventType + " " + aggregateId);

            await subject.SetEventPublished(eventType);
        }
    }
}
