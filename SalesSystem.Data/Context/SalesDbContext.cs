using Microsoft.EntityFrameworkCore;
using SalesSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Data.Context
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones
            modelBuilder.Entity<ProductStore>().HasKey(ps => new { ps.ProductId, ps.StoreId });
            modelBuilder.Entity<CustomerProduct>().HasKey(cp => new { cp.CustomerId, cp.ProductId, cp.PurchaseDate });
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            // Datos iniciales - Stores
            modelBuilder.Entity<Store>().HasData(
                new Store { StoreId = 1, BranchName = "Main Branch", Address = "123 Main Street" },
                new Store { StoreId = 2, BranchName = "West Branch", Address = "456 West Avenue" }
            );

            // Datos iniciales - Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Code = "P001", Description = "Laptop", Image = null, Price = 999.99m, Stock = 10 },
                new Product { ProductId = 2, Code = "P002", Description = "Smartphone", Image = null, Price = 599.99m, Stock = 25 }
            );

            // Datos iniciales - Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Smith", Address = "100 Apple St" },
                new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Johnson", Address = "200 Orange Ave" }
            );

            // Relaciones (ProductStore)
            modelBuilder.Entity<ProductStore>().HasData(
                new ProductStore { ProductId = 1, StoreId = 1, AssignedDate = new DateTime(2024, 1, 10) },
                new ProductStore { ProductId = 2, StoreId = 2, AssignedDate = new DateTime(2024, 1, 15) }
            );

            // Relaciones (CustomerProduct)
            modelBuilder.Entity<CustomerProduct>().HasData(
                new CustomerProduct { CustomerId = 1, ProductId = 1, PurchaseDate = new DateTime(2024, 2, 1) },
                new CustomerProduct { CustomerId = 2, ProductId = 2, PurchaseDate = new DateTime(2024, 3, 1) }
            );
        }
    }
}
