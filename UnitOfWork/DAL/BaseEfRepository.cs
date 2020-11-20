using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitOfWork.DAL
{
    public class BaseEfRepository<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext _db;
        protected DbSet<T> _dbSet;

        public BaseEfRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(params T[] entities)
        {
            _dbSet.AddRange(entities);
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