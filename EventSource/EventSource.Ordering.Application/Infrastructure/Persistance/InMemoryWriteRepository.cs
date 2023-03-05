using EventSource.Ordering.Application.Common.EventPublisher;
using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Domain.Common;
using System.Reflection;

namespace EventSource.Ordering.Infrastructure.Persistance
{
    public class InMemoryWriteRepository<TAggregate, TAggregateId> : IWriteRepository<TAggregate, TAggregateId> 
        where TAggregate : IAggregateRoot<TAggregateId>
        where TAggregateId : IAggregateId, new()
    {
        IDomainEventPublisher<TAggregateId> eventPublisher;

        public InMemoryWriteRepository(IDomainEventPublisher<TAggregateId> eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public async Task<IList<TAggregate>> GetAllAsync()
        {
            var ids = InMemoryWritePersistance<TAggregateId>.domainEvents.Select(x => x.AggregateId).Distinct();

            List<TAggregate> list = new();

            foreach (TAggregateId id in ids) 
            {
                var entity = await GetByIdAsync(id.ToString());
                list.Add(entity);
            }

            return list;
        }

        public async Task<TAggregate> GetByIdAsync(string id)
        {
            try
            {
                await Task.Delay(1);
                var aggregate = CreateEmptyAggregate();
                IESAggregateRoot<TAggregateId> aggregatePersistence = (IESAggregateRoot<TAggregateId>)aggregate;

                foreach (var @event in InMemoryWritePersistance<TAggregateId>.domainEvents.Where(x => x.AggregateId.ToString() == id).ToList())
                {
                    aggregatePersistence.ApplyEvent(@event, @event.AggregateVersion);
                }
                return aggregate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private TAggregate CreateEmptyAggregate()
        {
            return (TAggregate)typeof(TAggregate)
                    .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null, new Type[0], new ParameterModifier[0])
                    .Invoke(new object[0]);
            // return Activator.CreateInstance<TAggregate>();
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            var aggregatePersistance = (IESAggregateRoot<TAggregateId>)aggregate;

            foreach (var @event in aggregatePersistance.GetUncommittedEvents()) 
            {
                InMemoryWritePersistance<TAggregateId>.domainEvents.Add(@event);

                await eventPublisher.PublishEvent(@event.GetType().Name, @event.AggregateId.ToString(), GetEventAsMap(@event));

            }

            aggregatePersistance.ClearUncommittedEvents();

        }

        private IDictionary<string, object> GetEventAsMap(IDomainEvent<TAggregateId> @event)
        {
            var map = new Dictionary<string, object>();
            var properties = @event.GetType().GetProperties();
            foreach (var p in properties) if (!GetIgnoreProperties().Contains(p.Name)) map.Add(p.Name, p.GetValue(@event, null));
            return map;
        }

        private HashSet<string> GetIgnoreProperties()
        {
            var result = new HashSet<string>();
            result.Add("EventId");
            result.Add("AggregateId");
            result.Add("AggregateVersion");
            result.Add("EventDate");
            result.Add("EventType");
            return result;
        }
    }
}
