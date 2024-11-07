using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            _logger.LogInformation("Retrieving all customers.");
            var customers = await _customerService.GetCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            _logger.LogInformation($"Retrieving customer with ID {id}.");
            var customer = await _customerService.GetCustomerAsync(id);
            return Ok(customer);
        }
    }
}
