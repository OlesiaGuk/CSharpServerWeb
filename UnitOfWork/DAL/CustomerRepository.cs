using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public class CustomerRepository : BaseEfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext db) : base(db)
        {
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            return _dbSet.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

        public Dictionary<string, double> GetEveryCustomerCosts()
        {
            return _dbSet.Select(c => new
            {
                CustomerPhoneNumber = c.PhoneNumber,
                CustomerCosts = c.Orders.SelectMany(o => o.ProductOrders).Sum(po => po.ProductsAmount * po.Product.Price)

            })
                .ToDictionary(a => a.CustomerPhoneNumber, a => a.CustomerCosts);
        }
    }
}