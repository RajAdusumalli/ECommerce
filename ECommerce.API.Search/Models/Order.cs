namespace ECommerce.API.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = default!;
        public decimal Total { get; set; }
        public IList<OrderItem> Items { get; set; } = default!;
    }
}
