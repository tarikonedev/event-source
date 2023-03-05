using EventSource.Ordering.Application.Common.Commands;
using EventSource.Ordering.Application.Common.EventPublisher;
using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Application.Infrastructure.Persistance;
using EventSource.Ordering.Domain.Orders.Write;
using EventSource.Ordering.Infrastructure.Persistance;

namespace EventSource.Ordering.Application.Orders.Commands.Handlers
{
    internal class AddOrderLineItemCommandHandler : ICommandHandler<AddOrderLineItemCommand>
    {
        public async Task Handle(AddOrderLineItemCommand command)
        {
            IDomainEventPublisher<OrderId> publisher = new InMemoryDomainEventPublisher<OrderId>();

            IWriteRepository<Order, OrderId> repo = new InMemoryWriteRepository<Order, OrderId>(publisher);
            var orderAggregate = await repo.GetByIdAsync(command.OrderId);
            orderAggregate.AddOrderLineItem(command.ProductId, command.Qty, command.UnitPrice);
            await repo.SaveAsync(orderAggregate);
        }
    }
}
