namespace EventSource.Ordering.Application.Orders.Commands
{
    public class CreateOrderCommand
    {
        public CreateOrderCommand(string customerId, DateTime orderDate, string orderStatus)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
        }

        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
