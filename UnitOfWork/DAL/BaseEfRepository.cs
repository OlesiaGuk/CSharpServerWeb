using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitOfWork.DAL
{
    public class BaseEfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext _db;
        protected DbSet<T> _dbSet;

        public BaseEfRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual T[] GetAll()
        {
            return _dbSet.ToArray();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}