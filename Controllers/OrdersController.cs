using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using OrderProcessingSystem.Interface;

namespace OrderProcessingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(orderDto);
                var orderlist = await _orderService.GetOrderAsync(order.Id);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderlist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating order: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving order with ID {id}: {ex.Message}");
                return NotFound();
            }
        }
    }
}
