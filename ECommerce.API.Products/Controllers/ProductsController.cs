using ECommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsService.GetProductsAsync();
            if (result.IsSuccess)
                return Ok(result.Products);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productsService.GetProductAsync(id);
            if (result.IsSuccess)
                return Ok(result.Product);
            else
                return NotFound();
        }
    }
}
