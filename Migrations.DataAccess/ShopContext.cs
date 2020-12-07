using Microsoft.EntityFrameworkCore;
using Migrations.DataAccess.Models;

namespace Migrations.DataAccess
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=91.201.72.213;Database=ShopEFGuk2;User=study;Password=study;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(b =>
            {
                b.HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(c => c.CategoryId);

                b.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<ProductOrder>(b =>
            {
                b.HasOne(po => po.Order)
                    .WithMany(o => o.ProductOrders)
                    .HasForeignKey(o => o.OrderId);

                b.HasOne(po => po.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(p => p.ProductId);
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(b =>
            {
                b.Property(c => c.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    ;

                b.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                b.Property(c => c.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50);

                b.Property(c => c.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                b.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}