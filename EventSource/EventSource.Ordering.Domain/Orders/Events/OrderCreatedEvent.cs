using EventSource.Ordering.Domain.Common;
using EventSource.Ordering.Domain.Orders.Write;

namespace EventSource.Ordering.Domain.Orders.Events
{
    public class OrderCreatedEvent : DomainEventBase<OrderId>
    {
        public OrderCreatedEvent(OrderId orderId, string customerId, string orderStatus, DateTime orderDate):base(orderId)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
        }

        public OrderCreatedEvent(OrderId orderId, long aggregateVersion, string customerId, string orderStatus, DateTime orderDate)
        : base(orderId, aggregateVersion)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
        }

        public string CustomerId { get; private set; }

        public DateTime OrderDate { get; private set; }

        public string OrderStatus { get; private set; }

        public override IDomainEvent<OrderId> WithAggregate(OrderId aggregateId, long aggregateVersion)
        {
            return new OrderCreatedEvent(aggregateId, aggregateVersion, CustomerId, OrderStatus, OrderDate);
        }
    }
}
