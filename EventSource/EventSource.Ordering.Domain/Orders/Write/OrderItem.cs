namespace EventSource.Ordering.Domain.Orders.Write
{
    public class OrderLineItem
    {
        public OrderLineItem(string productId, int quantity, double unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
    }
}