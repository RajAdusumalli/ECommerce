using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, dynamic? Customer, string? ErrorMessage)> GetCustomerAsync(int id);
    }
}
