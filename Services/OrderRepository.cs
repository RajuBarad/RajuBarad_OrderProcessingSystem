using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(c=>c.Customer)
                                         .Include(o => o.OrderItems)
                                         .ThenInclude(oi => oi.Product)
                                         .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                         .ThenInclude(oi => oi.Product)
                                         .ToListAsync();
        }
    }
}
