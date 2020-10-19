using ShopEF.Models;

namespace ShopEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureCreated();

                var category1 = new Category { Name = "Фрукты" };
                var category2 = new Category { Name = "Овощи" };
                var category3 = new Category { Name = "Ягоды" };

                db.Categories.AddRange(category1, category2, category3);
                db.SaveChanges();

                var product1 = new Product { Name = "Огурцы", Price = 60 };
                var product2 = new Product { Name = "Бананы", Price = 90 };
                var product3 = new Product { Name = "Абрикосы", Price = 150 };
                var product4 = new Product { Name = "Картофель", Price = 50 };
                var product5 = new Product { Name = "Черешня", Price = 300 };

                db.Products.AddRange(product1, product2, product3, product4, product5);
                db.SaveChanges();

                product1.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product1.Id });
                product2.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product2.Id });
                product3.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product3.Id });
                product4.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product4.Id });
                product5.ProductCategories.Add(new ProductCategory { CategoryId = category3.Id, ProductId = product5.Id });

                db.SaveChanges();

                var customer1 = new Customer { Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", PhoneNumber = "12345", Email = "ivanov@mail.ru" };
                var customer2 = new Customer { Surname = "Петрова", Name = "Наталья", MiddleName = "Ивановна", PhoneNumber = "23456", Email = "ivanova@mail.ru" };
                var customer3 = new Customer { Surname = "Николаева", Name = "Валентина", MiddleName = "Ивановна", PhoneNumber = "34567", Email = "nikolaeva@mail.ru" };

                db.Customers.AddRange(customer1, customer2, customer3);
                db.SaveChanges();

                var order1 = new Order { Date = "2020-09-01", CustomerId = customer1.Id };
                var order2 = new Order { Date = "2020-09-05", CustomerId = customer1.Id };
                var order3 = new Order { Date = "2020-09-06", CustomerId = customer2.Id };
                var order4 = new Order { Date = "2020-09-10", CustomerId = customer3.Id };

                db.Orders.AddRange(order1, order2, order3, order4);
                db.SaveChanges();

                order1.ProductOrders.Add(new ProductOrder { OrderId = order1.Id, ProductId = product4.Id, ProductsAmount = 2 });
                order1.ProductOrders.Add(new ProductOrder { OrderId = order1.Id, ProductId = product1.Id, ProductsAmount = 1.5 });
                order2.ProductOrders.Add(new ProductOrder { OrderId = order2.Id, ProductId = product5.Id, ProductsAmount = 3 });
                order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product1.Id, ProductsAmount = 1 });
                order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product2.Id, ProductsAmount = 2 });
                order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product5.Id, ProductsAmount = 2.5 });
                order4.ProductOrders.Add(new ProductOrder { OrderId = order4.Id, ProductId = product5.Id, ProductsAmount = 1 });

                db.SaveChanges();
            }
        }
    }
}