using EventSource.Ordering.Domain.Common;
using EventSource.Ordering.Domain.Orders.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Domain.Orders.Events
{
    public class OrderLineItemAddedEvent : DomainEventBase<OrderId>
    {
        public string ProductId { get; private set; }
        public int Qty { get; private set; }
        public double UnitPrice { get; private set; }

        OrderLineItemAddedEvent()
        {
        }

        internal OrderLineItemAddedEvent(string productId, int qty, double unitPrice) : base()
        {
            this.ProductId = productId;
            this.Qty = qty;
            this.UnitPrice = unitPrice;
        }

        internal OrderLineItemAddedEvent(OrderId aggegateId, long aggregateVersion, string productId, int qty, double unitPrice)
            : base(aggegateId, aggregateVersion)
        {
            this.ProductId = productId;
            this.Qty = qty;
            this.UnitPrice = unitPrice;
        }

        public override IDomainEvent<OrderId> WithAggregate(OrderId aggregateId, long aggregateVersion)
        {
            return new OrderLineItemAddedEvent(aggregateId, aggregateVersion, ProductId, Qty, UnitPrice);
        }
    }
}
