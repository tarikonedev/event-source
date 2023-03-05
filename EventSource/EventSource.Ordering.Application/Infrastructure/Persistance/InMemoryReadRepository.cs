using EventSource.Ordering.Application.Common.Persistance;
using EventSource.Ordering.Domain.Orders.Read;
using EventSource.Ordering.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Infrastructure.Persistance
{
    public class InMemoryReadRepository : IReadRepository
    {
        public async Task UpdateReadModelOnEventPublish(string eventType)
        {
            await Task.Delay(1);
            //SimpleLogger.Log("updates for " + eventType);
            var latestPublishedEvent = InMemoryReadPersistance.PublishedEvents.OrderByDescending(x => x.EventDate).FirstOrDefault();
            switch (latestPublishedEvent.EventType)
            {
                case "OrderCreatedEvent":
                    if (InMemoryReadPersistance.ReadOrders.Where(x => x.AggregateId == latestPublishedEvent.AggregateId.ToString()).FirstOrDefault() == null)
                        InMemoryReadPersistance.ReadOrders.Add(new OrderRM(
                            latestPublishedEvent.AggregateId.ToString(),
                            latestPublishedEvent.EventData["CustomerId"].ToString(),
                            (DateTime)latestPublishedEvent.EventData["OrderDate"],
                            latestPublishedEvent.EventData["OrderStatus"].ToString()));
                    break;
                case "AddOrderLineItemEvent":
                    {
                        var order = InMemoryReadPersistance.ReadOrders.Where(x => x.AggregateId == latestPublishedEvent.AggregateId).FirstOrDefault();
                        if (order != null)
                        {
                            var updatedOrder = CopyOrder(order);
                            updatedOrder.Items.Add(new OrderLineItemRM()
                            {
                                ProductId = latestPublishedEvent.EventData["ProductId"].ToString(),
                                Qty = (int)latestPublishedEvent.EventData["Qty"],
                                UnitPrice = (double)latestPublishedEvent.EventData["UnitPrice"]
                            });
                            InMemoryReadPersistance.ReadOrders.Remove(order);
                            InMemoryReadPersistance.ReadOrders.Add(updatedOrder);
                        }
                    }
                    break;
              
            }
        }

        private static OrderRM CopyOrder(OrderRM order)
        {
            var updatedOrder = new OrderRM(order.AggregateId, order.CustomerId, order.OrderDate, order.OrderStatus);
            if (order.Items != null && order.Items.Count > 0) updatedOrder.Items = order.Items;
            return updatedOrder;
        }

        public IEnumerable<OrderRM> GetOrders()
        {
            return InMemoryReadPersistance.ReadOrders.AsEnumerable();
        }
    }
}
