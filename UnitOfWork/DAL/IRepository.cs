using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork.DAL
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);

        void Update(T entity);

        public void Delete(T entity);

        T[] GetAll();

        T GetById(int id);
    }
}