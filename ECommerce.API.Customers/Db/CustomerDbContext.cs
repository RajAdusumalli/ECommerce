using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Db
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; } = default!;
    }
}
