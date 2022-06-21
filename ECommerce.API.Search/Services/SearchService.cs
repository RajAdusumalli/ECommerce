using ECommerce.API.Search.Interfaces;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomerService customerService)
        {
            _ordersService = ordersService; 
            _productsService = productsService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var customerResult = await _customerService.GetCustomerAsync(customerId);
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            if(ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders!)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? 
                            productsResult.Products!.FirstOrDefault(p => p.Id == item.ProductId)?.Name! :
                            "Product information is not available";
                    }
                }

                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new { Name = "Customer informationis not available" },
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
