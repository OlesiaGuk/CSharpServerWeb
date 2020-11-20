using System;

namespace UnitOfWork.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : class, IRepository;
    }
}