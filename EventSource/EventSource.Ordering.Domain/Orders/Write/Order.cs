using EventSource.Ordering.Domain.Common;
using EventSource.Ordering.Domain.Orders.Events;

namespace EventSource.Ordering.Domain.Orders.Write
{
    public class Order: AggregateBase<OrderId>
    {
        private Order()
        {
            Items = new List<OrderLineItem>();
        }

        public Order(OrderId orderId, string customerId, string orderStatus, DateTime orderDate)
        {
            if(orderId == null) throw new ArgumentNullException(nameof(orderId));
            if (String.IsNullOrEmpty(customerId)) throw new ArgumentNullException(nameof(customerId));
            if (String.IsNullOrEmpty(orderStatus)) throw new ArgumentNullException(nameof(orderStatus));

            RaiseEvent(new OrderCreatedEvent(orderId, customerId, orderStatus, orderDate));
        }

        public string CustomerId { get; private set; }

        public string OrderStatus { get; private set; }

        public DateTime OrderDate { get; private set; }

        public List<OrderLineItem> Items { get; private set; }

       

        public void AddOrderLineItem(string productId, int qty, double unitPrice)
        {
            if (String.IsNullOrEmpty(productId)) throw new ArgumentNullException(nameof(productId));
            if (qty <= 0) throw new ArgumentException("Quantity cannot be zero or negative", nameof(qty));
            if (unitPrice <= 0) throw new ArgumentException("Unit Price cannot be zero or negative", nameof(unitPrice));

            RaiseEvent(new OrderLineItemAddedEvent(productId, qty, unitPrice));
        }

        internal void Apply(OrderCreatedEvent ev)
        {
            Id = ev.AggregateId;
            CustomerId = ev.CustomerId;
            OrderDate = ev.OrderDate;
            OrderStatus = ev.OrderStatus;
        }

        internal void Apply(OrderLineItemAddedEvent ev)
        {
            if (Items == null) Items = new List<OrderLineItem>();

            var item = new OrderLineItem(ev.ProductId, ev.Qty, ev.UnitPrice);

            Items.Add(item);
        }
    }
}
