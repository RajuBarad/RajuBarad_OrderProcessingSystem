using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
