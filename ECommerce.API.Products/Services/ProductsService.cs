using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public ProductsService(ProductsDbContext context, ILogger<ProductsService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!_context.Products.Any())
            {
                _context.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                _context.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                _context.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 100 });
                _context.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 200 });
                _context.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                else
                    return (true, null, "Not found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (true, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product? Product, string? ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null )
                {
                    var result = _mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                else
                    return (true, null, "Not found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (true, null, ex.Message);
            }
        }
    }
}
