using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Product GetProductByName(string productName);

        public Product GetMostOftenBuyProduct();
    }
}