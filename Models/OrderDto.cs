using System.Collections.Generic;

namespace OrderProcessingSystem.Models
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
