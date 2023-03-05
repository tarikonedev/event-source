using EventSource.Ordering.Domain.Common;

namespace EventSource.Ordering.Application.Common.Persistance
{
    public interface IWriteRepository<TAggregate, TAggregateId> where TAggregate : IAggregateRoot<TAggregateId>
    {
        Task SaveAsync(TAggregate aggregate);
        Task<TAggregate> GetByIdAsync(string id);
        Task<IList<TAggregate>> GetAllAsync();
    }
}
