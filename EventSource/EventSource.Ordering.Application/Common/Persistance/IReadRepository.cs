using EventSource.Ordering.Domain.Orders.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Common.Persistance
{
    public interface IReadRepository
    {
        IEnumerable<OrderRM> GetOrders();

        Task UpdateReadModelOnEventPublish(string eventType);
    }
}
