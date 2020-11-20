using System.Collections.Generic;
using UnitOfWork.Models;

namespace UnitOfWork.DAL
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Customer GetCustomerByPhoneNumber(string phoneNumber);

        Dictionary<string, double> GetEveryCustomerCosts();
    }
}