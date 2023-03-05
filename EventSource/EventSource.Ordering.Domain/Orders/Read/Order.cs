using EventSource.Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Domain.Orders.Read
{
    public class OrderRM : BaseReadModel
    {


        public OrderRM(string id, string customerId, DateTime orderDate, string orderStatus)
        {
            this.AggregateId = id;
            this.CustomerId = customerId;
            this.OrderDate = orderDate;
            this.OrderStatus = orderStatus;
            Items = new List<OrderLineItemRM>();
        }

        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public IList<OrderLineItemRM> Items { get; set; }
        public override string ToString()
        {
            var buf = new System.Text.StringBuilder();
            foreach (var i in Items) buf.Append(i.ToString());
            return CustomerId + " " + OrderDate + " " + OrderStatus + " " + buf.ToString();
        }

    }
    public class OrderLineItemRM
    {
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
        //public override string ToString() => ModelHelper.ToStringHelper(this);
    }
}
