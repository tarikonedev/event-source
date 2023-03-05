using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSource.Ordering.Application.Orders.Commands
{
    public class AddOrderLineItemCommand
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Qty { get; set; }
        public double UnitPrice { get; set; }
    }
}
