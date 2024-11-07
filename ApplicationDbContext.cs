using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderProcessingSystem
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(oi => oi.Order)
            //    .WithMany(o => o.OrderItems)
            //    .HasForeignKey(oi => oi.OrderId);

            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(oi => oi.Product)
            //    .WithMany()
            //    .HasForeignKey(oi => oi.ProductId);
        }
    }
}
