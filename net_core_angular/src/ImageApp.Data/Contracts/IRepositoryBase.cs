using System.Collections.Generic;

namespace ImageApp.Data.Contracts
{
    public interface IRepositoryBase<T>
    {
        T Create(T entity);
        T GetItem(int id, string include = null);
        IEnumerable<T> Read();
        IEnumerable<T> Read(string include);
        T Update(int id, T entity);
        T Delete(T entity);
        T Delete(int id);
    }
}
