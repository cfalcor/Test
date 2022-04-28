using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test
{
    public class ProductManagmentContext : DbContext
    {
        public ProductManagmentContext(DbContextOptions<ProductManagmentContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
        }
    }
}
