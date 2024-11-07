using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ApplicationDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            
            return await _context.Customers.Include(c => c.Orders)
                                            .ThenInclude(o => o.OrderItems)
                                            .ThenInclude(oi => oi.Product)
                                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
