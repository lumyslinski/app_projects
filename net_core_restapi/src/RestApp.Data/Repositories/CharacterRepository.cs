using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestApp.Data.Contracts;
using RestApp.Data.Database;
using RestApp.Data.Models;

namespace RestApp.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CharacterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Create(CharacterModelDatabase character)
        {
            dbContext.Characters.Add(character);
            dbContext.SaveChanges();
            return character.Id;
        }

        public void Delete(int id)
        {
            var foundToDelete = dbContext.Characters.FirstOrDefault(c => c.Id == id);
            if (foundToDelete != null)
            {
                dbContext.Characters.Remove(foundToDelete);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<CharacterModelDatabase> Read()
        {
            var characters = dbContext.Characters.Include(e => e.Episodes).ThenInclude(ee => ee.Episode)
                                                 .Include(f => f.Friends).AsEnumerable();
            return characters;
        }

        public void Update(CharacterModelDatabase item)
        {
            dbContext.Characters.Update(item);
            dbContext.SaveChanges();
        }

        public CharacterModelDatabase GetItem(int id)
        {
            return dbContext.Characters.FirstOrDefault(c => c.Id == id);
        }
    }
}
