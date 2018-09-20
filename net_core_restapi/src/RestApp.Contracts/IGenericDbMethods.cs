using System.Collections.Generic;

namespace RestApp.Contracts
{
    public interface IGenericDbMethods<T>
    {
        void Create(T item);
        IEnumerable<T> Read();
        void Update(T item);
        void Delete(int id);
    }
}
