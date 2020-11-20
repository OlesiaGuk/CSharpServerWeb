namespace UnitOfWork.DAL
{
    public interface IRepositoryBase<T> : IRepository where T : class
    {
        void Add(T entity);

        void AddRange(params T[] entities);

        void Update(T entity);

        public void Delete(T entity);

        T[] GetAll();

        T GetById(int id);
    }
}