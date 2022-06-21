using ECommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customerService.GetCustomersAsync();
            if (result.IsSuccess)
                return Ok(result.Customers);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customerService.GetCustomerAsync(id);
            if (result.IsSuccess)
                return Ok(result.Customer);
            else
                return NotFound();
        }
    }
}
