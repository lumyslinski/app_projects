using System.Collections.Generic;

namespace RestApp.Data.Contracts
{
    public interface IGenericRepository<T>
    {
        int Create(T item);
        IEnumerable<T> Read();
        void Update(T item);
        void Delete(int id);
        T GetItem(int id);
    }
}
