namespace ECommerce.API.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = default!;
        public decimal Total { get; set; }
        public IList<OrderItem> Items { get; set; } = default!;
    }
}
