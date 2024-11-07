using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Interface
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(int id);
        
        Task<IEnumerable<Customer>> GetCustomersAsync();
    }
}
