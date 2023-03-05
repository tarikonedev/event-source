using EventSource.Ordering.Application.Common.Commands;
using EventSource.Ordering.Application.Common.EventPublisher;
using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Application.Infrastructure.Persistance;
using EventSource.Ordering.Domain.Orders.Write;
using EventSource.Ordering.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Orders.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand command)
        {
            var aggregateId = new OrderId();
            var order = new Order(aggregateId, command.CustomerId, command.OrderStatus, command.OrderDate);

            IDomainEventPublisher<OrderId> publisher = new InMemoryDomainEventPublisher<OrderId>();
            IWriteRepository<Order, OrderId> repo = new InMemoryWriteRepository<Order, OrderId>(publisher);

            await repo.SaveAsync(order);
        }
    }
}
