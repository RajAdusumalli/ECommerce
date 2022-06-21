namespace ECommerce.API.Products.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
