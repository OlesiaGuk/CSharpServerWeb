using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public Product GetProductByName(string productName)
        {
            return _dbSet.FirstOrDefault(p => p.Name == productName);
        }

        public Product GetMostOftenBuyProduct()
        {
            return _dbSet
                .OrderByDescending(p => p.ProductOrders.Sum(po => po.ProductsAmount))
                .First();
        }
    }
}