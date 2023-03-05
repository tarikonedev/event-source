using EventSource.Ordering.Application.Common.EventPublisher;
using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Application.Infrastructure.Persistance;
using EventSource.Ordering.Domain.Orders.Write;
using EventSource.Ordering.Infrastructure.Persistance;

namespace EventSource.Ordering.Application.Orders
{
    public class OrderService
    {
        public async Task<IList<Order>> GetAll() 
        {
            IDomainEventPublisher<OrderId> publisher = new InMemoryDomainEventPublisher<OrderId>();

            IWriteRepository<Order, OrderId> repo = new InMemoryWriteRepository<Order, OrderId>(publisher);

            return await repo.GetAllAsync(); 
        }

    }
}
