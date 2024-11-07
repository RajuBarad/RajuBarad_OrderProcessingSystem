using System.Collections.Generic;
using System;
using System.Linq;

namespace OrderProcessingSystem.Models
{
    // Models/Order.cs
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        // Total price will be calculated dynamically
        public decimal TotalPrice => OrderItems.Sum(item => item.Product.Price * item.Quantity);

        // Business rule: Can't create order if previous order is unfulfilled
        public bool IsFulfilled { get; set; }
    }
}
