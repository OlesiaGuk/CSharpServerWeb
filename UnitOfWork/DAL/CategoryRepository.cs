using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }

        public Dictionary<string, double> GetBoughtProductsAmountByCategories()
        {
            return _dbSet.Select(c => new
            {
                CategoryName = c.Name,
                SoldAmount = c.ProductCategories.SelectMany(pc => pc.Product.ProductOrders).Sum(po => po.ProductsAmount)
            })
                .ToDictionary(a => a.CategoryName, a => a.SoldAmount);
        }
    }
}