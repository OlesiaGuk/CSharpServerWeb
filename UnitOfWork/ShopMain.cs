using System;
using UnitOfWork.Models;
using UnitOfWork.DAL;

namespace UnitOfWork
{
    class ShopMain
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                db.Database.EnsureCreated();
            }

            FillDatabase();

            var searchedPhoneNumber = "12345";
            var editedPhoneNumber = "15555";
            EditCustomerPhoneNumber(searchedPhoneNumber, editedPhoneNumber);

            var deletingProductName = "Вишня";
            DeleteProduct(deletingProductName);

            PrintMostOftenBuyProduct();

            PrintEveryCustomerCosts();

            PrintBoughtProductsAmountByCategories();

            AddProductWithTransaction();
        }

        public static void AddProductWithTransaction()
        {
            using var db = new ShopContext();
            using var dbTransaction = db.Database.BeginTransaction();

            try
            {
                var newCategory = new Category { Name = "Орехи" };
                db.Categories.Add(newCategory);
                db.SaveChanges();

                var newProduct = new Product { Name = "Миндаль", Price = 400 };
                db.Products.Add(newProduct);
                db.SaveChanges();

                newProduct.ProductCategories.Add(new ProductCategory { CategoryId = newCategory.Id, ProductId = newProduct.Id });
                db.SaveChanges();

                dbTransaction.Commit();
            }
            catch (Exception)
            {
                dbTransaction.Rollback();
            }
        }

        public static void FillDatabase()
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());

            var categoryRepo = uow.GetRepository<ICategoryRepository>();
            var productRepo = uow.GetRepository<IProductRepository>();
            var customerRepo = uow.GetRepository<ICustomerRepository>();
            var orderRepo = uow.GetRepository<IOrderRepository>();

            var category1 = new Category { Name = "Фрукты" };
            var category2 = new Category { Name = "Овощи" };
            var category3 = new Category { Name = "Ягоды" };

            categoryRepo.AddRange(category1, category2, category3);
            uow.Save();

            var product1 = new Product { Name = "Огурцы", Price = 60 };
            var product2 = new Product { Name = "Бананы", Price = 90 };
            var product3 = new Product { Name = "Абрикосы", Price = 150 };
            var product4 = new Product { Name = "Картофель", Price = 50 };
            var product5 = new Product { Name = "Черешня", Price = 300 };
            var product6 = new Product { Name = "Вишня", Price = 250 };

            productRepo.AddRange(product1, product2, product3, product4, product5, product6);
            uow.Save();

            product1.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product1.Id });
            product2.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product2.Id });
            product3.ProductCategories.Add(new ProductCategory { CategoryId = category1.Id, ProductId = product3.Id });
            product4.ProductCategories.Add(new ProductCategory { CategoryId = category2.Id, ProductId = product4.Id });
            product5.ProductCategories.Add(new ProductCategory { CategoryId = category3.Id, ProductId = product5.Id });
            product6.ProductCategories.Add(new ProductCategory { CategoryId = category3.Id, ProductId = product6.Id });

            uow.Save();

            var customer1 = new Customer { Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", PhoneNumber = "12345", Email = "ivanov@mail.ru" };
            var customer2 = new Customer { Surname = "Петрова", Name = "Наталья", MiddleName = "Ивановна", PhoneNumber = "23456", Email = "ivanova@mail.ru" };
            var customer3 = new Customer { Surname = "Николаева", Name = "Валентина", MiddleName = "Ивановна", PhoneNumber = "34567", Email = "nikolaeva@mail.ru" };

            customerRepo.AddRange(customer1, customer2, customer3);
            uow.Save();

            var order1 = new Order { Date = new DateTime(2020, 09, 01), CustomerId = customer1.Id };
            var order2 = new Order { Date = new DateTime(2020, 09, 05), CustomerId = customer1.Id };
            var order3 = new Order { Date = new DateTime(2020, 09, 05), CustomerId = customer2.Id };
            var order4 = new Order { Date = new DateTime(2020, 09, 10), CustomerId = customer3.Id };

            orderRepo.AddRange(order1, order2, order3, order4);
            uow.Save();

            order1.ProductOrders.Add(new ProductOrder { OrderId = order1.Id, ProductId = product4.Id, ProductsAmount = 2 });
            order1.ProductOrders.Add(new ProductOrder { OrderId = order1.Id, ProductId = product1.Id, ProductsAmount = 1.5 });
            order2.ProductOrders.Add(new ProductOrder { OrderId = order2.Id, ProductId = product5.Id, ProductsAmount = 3 });
            order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product1.Id, ProductsAmount = 1 });
            order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product2.Id, ProductsAmount = 2 });
            order3.ProductOrders.Add(new ProductOrder { OrderId = order3.Id, ProductId = product5.Id, ProductsAmount = 2.5 });
            order4.ProductOrders.Add(new ProductOrder { OrderId = order4.Id, ProductId = product5.Id, ProductsAmount = 1 });

            uow.Save();
        }

        public static void EditCustomerPhoneNumber(string searchedPhoneNumber, string editedPhoneNumber)
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());
            var customerRepo = uow.GetRepository<ICustomerRepository>();

            var searchedCustomer = customerRepo.GetCustomerByPhoneNumber(searchedPhoneNumber);

            if (searchedCustomer != null)
            {
                Console.WriteLine($"Данные покупателя: {searchedCustomer.Surname} {searchedCustomer.Name}, телефон - {searchedCustomer.PhoneNumber}");

                searchedCustomer.PhoneNumber = editedPhoneNumber;
                uow.Save();

                Console.WriteLine($"Данные изменены: {searchedCustomer.Surname} {searchedCustomer.Name}, телефон - {searchedCustomer.PhoneNumber}");
            }
        }

        public static void DeleteProduct(string productName)
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());
            var productRepo = uow.GetRepository<IProductRepository>();

            var deletingProduct = productRepo.GetProductByName(productName);

            if (deletingProduct != null)
            {
                productRepo.Delete(deletingProduct);
                uow.Save();
                Console.WriteLine("Товар удален");
            }
        }

        public static void PrintMostOftenBuyProduct()
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());
            var productRepo = uow.GetRepository<IProductRepository>();

            Console.WriteLine($"Самый часто покупаемый товар - {productRepo.GetMostOftenBuyProduct().Name}");
            Console.WriteLine();
        }

        public static void PrintEveryCustomerCosts()
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());
            var customerRepo = uow.GetRepository<ICustomerRepository>();

            var customerCostsDictionary = customerRepo.GetEveryCustomerCosts();

            Console.WriteLine("Потрачено каждым клиентом за все время:");

            foreach (var (key, value) in customerCostsDictionary)
            {
                Console.WriteLine($"Номер телефона - {key}, потрачено - {value} руб.");
            }

            Console.WriteLine();
        }

        public static void PrintBoughtProductsAmountByCategories()
        {
            using var uow = new DAL.UnitOfWork(new ShopContext());
            var categoryRepo = uow.GetRepository<ICategoryRepository>();

            var productsAmountByCategoriesDictionary = categoryRepo.GetBoughtProductsAmountByCategories();

            Console.WriteLine("Продано товаров по категориям: ");

            foreach (var (key, value) in productsAmountByCategoriesDictionary)
            {
                Console.WriteLine($"{key} - {value} кг.");
            }
        }
    }
}