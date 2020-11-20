using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public class OrderRepository : BaseEfRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext db) : base(db)
        {
        }
    }
}