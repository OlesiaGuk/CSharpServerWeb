using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork
{
    class ShopContext : DbContext
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
                .UseSqlServer("Server=91.201.72.213;Database=ShopEFGuk;User=study;Password=study;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(b =>
            {
                b.HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.CategoryId);

                b.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(pc => pc.ProductId);
            });

            modelBuilder.Entity<ProductOrder>(b =>
            {
                b.HasOne(po => po.Order)
                    .WithMany(o => o.ProductOrders)
                    .HasForeignKey(po => po.OrderId);

                b.HasOne(po => po.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(po => po.ProductId);
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.HasOne(o => o.Customer) // выбираем навигационное свойство для настройки
                    .WithMany(c => c.Orders) // указываем обратное навигационное свойство-коллекцию
                    .HasForeignKey(o => o.CustomerId); // указываем поле для внешнего ключа
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.Property(c => c.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.Property(p => p.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Customer>(b =>
            {
                b.Property(c => c.Surname)
                    .IsRequired();

                b.Property(c => c.Name)
                    .IsRequired();

                b.Property(c => c.MiddleName)
                    .IsRequired();

                b.Property(c => c.PhoneNumber)
                    .IsRequired();

                b.Property(c => c.Email)
                    .IsRequired();
            });
        }
    }
}