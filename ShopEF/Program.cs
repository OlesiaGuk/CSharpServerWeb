using System;
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

                db.Categories.Add(new Category { Name = "Фрукты" });
                db.Categories.Add(new Category { Name = "Овощи" });
                db.Categories.Add(new Category { Name = "Ягоды" });

                db.Products.Add(new Product { Name = "Огурцы", Price = 60, CategoryId = 2 });
                db.Products.Add(new Product { Name = "Бананы", Price = 90, CategoryId = 1 });
                db.Products.Add(new Product { Name = "Абрикосы", Price = 150, CategoryId = 1 });
                db.Products.Add(new Product { Name = "Картофель", Price = 50, CategoryId = 2 });
                db.Products.Add(new Product { Name = "Черешня", Price = 300, CategoryId = 3 });

                db.Customers.Add(new Customer { Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", PhoneNumber = "12345", Email = "ivanov@mail.ru" });
                db.Customers.Add(new Customer { Surname = "Петрова", Name = "Наталья", MiddleName = "Ивановна", PhoneNumber = "23456", Email = "ivanova@mail.ru" });
                db.Customers.Add(new Customer { Surname = "Николаева", Name = "Валентина", MiddleName = "Ивановна", PhoneNumber = "34567", Email = "nikolaeva@mail.ru" });



                db.SaveChanges();
            }
        }
    }
}
