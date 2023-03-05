using EventSource.Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Domain.Orders.Write
{
    public class OrderId : IAggregateId
    {
        public OrderId()
        {
            Id = Guid.NewGuid();
        }

        public OrderId(string aggregateIdString)
        {
            Id = Guid.Parse(aggregateIdString);
        }
        public Guid Id { get; private set; }

        public string IdAsString()
        {
            return Id.ToString();
        }

        public override string ToString() => IdAsString();
    }
}
