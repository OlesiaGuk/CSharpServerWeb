using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ShopEF.Models;

namespace ShopEF
{
    class ShopMain
    {
        static void Main(string[] args)
        {
            //FillDatabase();

            //var searchedPhoneNumber = "12345";
            //var editedPhoneNumber = "12245";
            //EditCustomerPhoneNumber(searchedPhoneNumber, editedPhoneNumber);

            //var deletingProductName = "Вишня";
            //DeleteProduct(deletingProductName);

            // Console.WriteLine("Самый часто покупаемый товар - " + GetMostOftenBuyProduct());

            GetEveryCustomerCosts();

            //GetBoughtProductsAmountByCategories();
        }

        public static void FillDatabase()
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
                var product6 = new Product { Name = "Вишня", Price = 250 };

                db.Products.AddRange(product1, product2, product3, product4, product5, product6);
                db.SaveChanges();

                product1.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product1.Id });
                product2.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product2.Id });
                product3.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product3.Id });
                product4.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product4.Id });
                product5.ProductCategories.Add(new ProductCategory { CategoryId = category3.Id, ProductId = product5.Id });
                product6.ProductCategories.Add(new ProductCategory { CategoryId = category3.Id, ProductId = product6.Id });

                db.SaveChanges();

                var customer1 = new Customer { Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", PhoneNumber = "12345", Email = "ivanov@mail.ru" };
                var customer2 = new Customer { Surname = "Петрова", Name = "Наталья", MiddleName = "Ивановна", PhoneNumber = "23456", Email = "ivanova@mail.ru" };
                var customer3 = new Customer { Surname = "Николаева", Name = "Валентина", MiddleName = "Ивановна", PhoneNumber = "34567", Email = "nikolaeva@mail.ru" };

                db.Customers.AddRange(customer1, customer2, customer3);
                db.SaveChanges();

                var order1 = new Order { Date = "2020-09-01", CustomerId = customer1.Id };
                var order2 = new Order { Date = "2020-09-05", CustomerId = customer1.Id };
                var order3 = new Order { Date = "2020-09-05", CustomerId = customer2.Id };
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

        public static void EditCustomerPhoneNumber(string searchedPhoneNumber, string editedPhoneNumber)
        {
            using (var db = new ShopContext())
            {
                var searchedCustomer = db.Customers.FirstOrDefault(c => c.PhoneNumber == searchedPhoneNumber);

                if (searchedCustomer != null)
                {
                    searchedCustomer.PhoneNumber = editedPhoneNumber;
                    var saveCommandResult = db.SaveChanges();

                    Console.WriteLine("Отредактировано: " + saveCommandResult);
                }
            }
        }

        public static void DeleteProduct(string productName)
        {
            using (var db = new ShopContext())
            {
                var deletingProduct = db.Products.FirstOrDefault(p => p.Name == productName);

                if (deletingProduct != null)
                {
                    db.Products.Remove(deletingProduct);
                    var saveCommandResult = db.SaveChanges();

                    Console.WriteLine("Удалено: " + saveCommandResult);
                }
            }
        }

        public static string GetMostOftenBuyProduct()
        {
            using (var db = new ShopContext())
            {
                var id = db.ProductOrders
                    .GroupBy(po => po.ProductId)
                    .Select(g => new { Id = g.Key, Value = g.Count() })
                    .OrderByDescending(g => g.Value)
                    .First()
                    .Id;

                var productName = db.Products
                    .Single(p => p.Id == id)
                    .Name;

                return productName;
            }
        }

        //Найти сколько каждый клиент потратил денег за все время
        public static double GetEveryCustomerCosts()
        {
            using (var db = new ShopContext())
            {
                //var x = db.Customers
                //    .GroupBy(c => c.Id)
                //    .Select(g => new
                //        {Id = g.Key, Value = g.Select(p => p.Orders.Select(o => o.ProductOrders.Sum(i => i.ProductsAmount * i.Product.Price)))})
                 
                //    ;

              //  x.ForEach(Console.WriteLine);
              return 0;
            }
        }

        // Вывести сколько товаров каждой категории купили
        public static void GetBoughtProductsAmountByCategories()
        {
            using (var db = new ShopContext())
            {
                //var x = db.ProductOrders
                //    .GroupBy(po => po.Product.)
                //    .Select(g => new { Id = g.Key, Value = g.Sum(p => p.ProductsAmount) })
                //    //.ToList()
                //    ;

                //var y = db.ProductCategories
                //    .GroupBy(pc => pc.CategoryId)
                //    .Select(g => new {Id = g.Key, Value = });

                //// x.ForEach(Console.WriteLine);

                //var y = x.Single(p => p.Id == 5).Value;
                //y.ForEach(Console.WriteLine);
                //   Console.WriteLine(y);

                var z = db.ProductCategories
                    .GroupBy(pc => pc.CategoryId)
                    .Select(g => new { Id = g.Key, Value = g.Select(p => p.Product) })
                    .ToList()
                    ;

                z.ForEach(Console.WriteLine);
            }
        }
    }
}