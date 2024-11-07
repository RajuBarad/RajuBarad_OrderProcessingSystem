using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Interface;
using OrderProcessingSystem.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace OrderProcessingSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(orderDto.CustomerId);
            if (customer == null)
            {
                _logger.LogError($"Customer with ID {orderDto.CustomerId} not found.");
                throw new ArgumentException("Customer not found.");
            }

            // Check if the customer has an unfulfilled order
            if (customer.Orders.Any(o => !o.IsFulfilled))
            {
                _logger.LogWarning($"Cannot place order for customer {orderDto.CustomerId}. A previous order is unfulfilled.");
                throw new InvalidOperationException("Cannot place order, previous order is unfulfilled.");
            }

            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                OrderItems = orderDto.ProductIds.Select(pid => new OrderItem 
                { 
                    ProductId = pid, 
                    Quantity = 1
                }).ToList()
            };

            var createdOrder = await _orderRepository.AddOrderAsync(order);
            _logger.LogInformation($"Order created successfully for customer {orderDto.CustomerId}, Order ID: {createdOrder.Id}");
            return createdOrder;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                _logger.LogError($"Order with ID {id} not found.");
                throw new ArgumentException("Order not found.");
            }

            return order;
        }
    }
}
