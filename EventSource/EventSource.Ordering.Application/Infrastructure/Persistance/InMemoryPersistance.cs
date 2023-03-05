using EventSource.Ordering.Application.Infrastructure.Persistance;
using EventSource.Ordering.Domain.Common;
using EventSource.Ordering.Domain.Orders.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Infrastructure.Persistance
{
    public class InMemoryWritePersistance<TAggregateId>
    {
        public static IList<IDomainEvent<TAggregateId>> domainEvents = new List<IDomainEvent<TAggregateId>>();

    }

    public class InMemoryReadPersistance
    {
        public static IList<MemoryPersistanceDomainEventModel> PublishedEvents = new List<MemoryPersistanceDomainEventModel>();
        public static IList<OrderRM> ReadOrders = new List<OrderRM>();
    }
}
