using System.Collections.Generic;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Dictionary<string, double> GetBoughtProductsAmountByCategories();
    }
}