using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestApp.Data.Contracts;
using RestApp.Data.Database;
using RestApp.Data.Models;

namespace RestApp.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDbContext dbContext;

        public FriendRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(FriendModelDatabase item)
        {
            this.dbContext.Friends.Add(item);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var foundToDelete = dbContext.Friends.FirstOrDefault(c => c.Id == id);
            if (foundToDelete != null)
            {
                dbContext.Friends.Remove(foundToDelete);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<FriendModelDatabase> Read()
        {
            return dbContext.Friends.AsEnumerable();
        }

        public void Update(FriendModelDatabase item)
        {
            dbContext.Friends.Update(item);
            dbContext.SaveChanges();
        }
    }
}
