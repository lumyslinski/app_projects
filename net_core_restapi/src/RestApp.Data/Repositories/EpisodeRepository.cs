using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestApp.Data.Contracts;
using RestApp.Data.Database;
using RestApp.Data.Models;

namespace RestApp.Data.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EpisodeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Create(EpisodeModelDatabase item)
        {
            this.dbContext.Episodes.Add(item);
            dbContext.SaveChanges();
            return item.Id;
        }

        public void Delete(int id)
        {
            var foundToDelete = dbContext.Episodes.FirstOrDefault(c => c.Id == id);
            if (foundToDelete != null)
            {
                dbContext.Episodes.Remove(foundToDelete);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<EpisodeModelDatabase> Read()
        {
            return dbContext.Episodes.AsEnumerable();
        }

        public void Update(EpisodeModelDatabase item)
        {
            dbContext.Episodes.Update(item);
            dbContext.SaveChanges();
        }

        public EpisodeModelDatabase GetItem(int id)
        {
            return dbContext.Episodes.FirstOrDefault(c => c.Id == id);
        }
    }
}
