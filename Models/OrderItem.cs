using System.Text.Json.Serialization;

namespace OrderProcessingSystem.Models
{
    // Models/OrderItem.cs (representing many-to-many relationship between Orders and Products)
    public class OrderItem
    {
        public int OrderId { get; set; }
      
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
