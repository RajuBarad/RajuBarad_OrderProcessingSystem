using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OrderProcessingSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        
        public async Task<Customer> GetCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                _logger.LogError($"Customer with ID {id} not found.");
                throw new ArgumentException("Customer not found.");
            }

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return customers;
        }

    }
}
