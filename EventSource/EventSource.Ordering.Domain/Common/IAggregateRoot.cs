namespace EventSource.Ordering.Domain.Common
{
    public interface IAggregateRoot<TId>
    {
        TId Id { get; }
    }
}
