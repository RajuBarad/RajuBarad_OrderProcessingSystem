using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrderProcessingSystem.Models
{
    // Models/Customer.cs
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation property
        
        public List<Order> Orders { get; set; }
    }
}
