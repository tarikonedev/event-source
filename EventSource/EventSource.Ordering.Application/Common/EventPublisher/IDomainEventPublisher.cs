namespace EventSource.Ordering.Application.Common.EventPublisher
{
    public interface IDomainEventPublisher<TAggregateId>
    {
        Task PublishEvent(string eventType, string aggregateId, IDictionary<string, object> eventData);
    }
}
