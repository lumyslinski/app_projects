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
                // firstly remove all references
                foreach (var episode in foundToDelete.Episodes)
                {
                    dbContext.CharacterEpisodes.Remove(episode);
                }
                foreach (var friend in foundToDelete.Friends)
                {
                    dbContext.CharacterFriends.Remove(friend);
                }
                dbContext.Characters.Remove(foundToDelete);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<CharacterModelDatabase> Read()
        {
            var characters = dbContext.Characters.Include(e => e.Episodes).ThenInclude(ee => ee.Episode)
                                                 .Include(f => f.Friends).ThenInclude(ff => ff.Friend).AsEnumerable();
            return characters;
        }

        public void Update(CharacterModelDatabase item)
        {
            dbContext.Characters.Update(item);
            dbContext.SaveChanges();
        }

        public void DeleteCharacterEpisode(CharacterEpisodeModelDatabase item)
        {
            dbContext.CharacterEpisodes.Remove(item);
        }

        public void DeleteCharacterFriend(CharacterFriendModelDatabase item)
        {
            dbContext.CharacterFriends.Remove(item);
        }

        public CharacterModelDatabase GetItem(int id)
        {
            return dbContext.Characters.Include(e => e.Episodes).ThenInclude(ee => ee.Episode)
                .Include(f => f.Friends).ThenInclude(ff => ff.Friend).FirstOrDefault(c => c.Id == id);
        }
    }
}
