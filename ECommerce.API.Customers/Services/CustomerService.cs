using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CustomerService(CustomerDbContext context, ILogger<CustomerService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Db.Customer() { Id = 1, Name = "Rajesh", Address = "8474 Dunham Station Dr, Tampa, FL 33647" });
                _context.Customers.Add(new Db.Customer() { Id = 2, Name = "Sirisha", Address = "8472 Dunham Station Dr, Tampa, FL 33647" });
                _context.Customers.Add(new Db.Customer() { Id = 3, Name = "Arjun", Address = "8470 Dunham Station Dr, Tampa, FL 33647" });
                _context.Customers.Add(new Db.Customer() { Id = 4, Name = "Siddharth", Address = "8474 Dunham Station Dr, Tampa, FL 33647" });
                _context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                _logger?.LogInformation("Querying customers");
                var customers = await _context.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    _logger?.LogInformation($"{customers.Count} customers(s) found");
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                else
                    return (false, null, "Not found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer? Customer, string? ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                _logger?.LogInformation("Querying customer");
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    _logger?.LogInformation("Customer found");
                    var result = _mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                else
                    return (false, null, "Not found.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
