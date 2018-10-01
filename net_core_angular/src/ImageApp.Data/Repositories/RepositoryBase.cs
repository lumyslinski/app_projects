using System;
using System.Collections.Generic;
using System.Linq;
using ImageApp.Data.Contracts;
using ImageApp.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public T Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            RepositoryContext.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Read()
        {
            return RepositoryContext.Set<T>().ToList();
        }

        public IEnumerable<T> Read(string include)
        {
            return RepositoryContext.Set<T>().Include(include).ToList();
        }

        public T Delete(T item)
        {
            RepositoryContext.Set<T>().Remove(item);
            RepositoryContext.SaveChanges();
            return item;
        }

        public T Delete(int id)
        {
            var foundItem = GetItem(id);
            if (foundItem != null)
                Delete(foundItem);
            return foundItem;
        }

        public T Update(int id, T entity)
        {
            var foundItem = GetItem(id);
            if (foundItem != null)
            {
                RepositoryContext.Entry(foundItem).CurrentValues.SetValues(entity);
                RepositoryContext.SaveChanges();
            }
            return foundItem;
        }

        public T GetItem(int id, string include=null)
        {
            if(String.IsNullOrEmpty(include))
                return RepositoryContext.Set<T>().Find(id);
            else
            {
                var found = RepositoryContext.Set<T>().Find(id);
                RepositoryContext.Entry(found).Reference(include).Load();
                return found;
            }
        }
    }
}
