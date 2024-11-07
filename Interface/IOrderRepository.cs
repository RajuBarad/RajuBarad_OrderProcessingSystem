using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Interface
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
