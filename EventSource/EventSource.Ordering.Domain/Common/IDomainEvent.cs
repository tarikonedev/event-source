namespace EventSource.Ordering.Domain.Common
{
    public interface IDomainEvent<TAggregateId>
    {
        Guid EventId { get; }
        TAggregateId AggregateId { get; }
        long AggregateVersion { get; }
        DateTime EventDate { get; }
        string EventType { get; }
    }
}
