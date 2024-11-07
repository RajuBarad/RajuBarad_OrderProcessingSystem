using OrderProcessingSystem.Models;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderDto orderDto);
        Task<Order> GetOrderAsync(int id);
    }
}
