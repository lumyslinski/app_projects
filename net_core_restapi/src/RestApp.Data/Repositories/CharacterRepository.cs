using System;
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

        public void CreateRange(List<CharacterModelDatabase> newCharacters)
        {
            dbContext.Characters.AddRange(newCharacters);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var foundToDelete = dbContext.Characters.Include(e => e.Episodes).ThenInclude(ee => ee.Episode)
                                                    .Include(f => f.Friends).ThenInclude(ff => ff.Friend).FirstOrDefault(c => c.Id == id);
            if (foundToDelete != null)
            {
                // firstly remove all references
                if (foundToDelete.Episodes != null)
                {
                    dbContext.CharacterEpisodes.RemoveRange(foundToDelete.Episodes);
                    dbContext.SaveChanges();
                }

                if (foundToDelete.Friends != null)
                {
                    dbContext.CharacterFriends.RemoveRange(foundToDelete.Friends);
                    dbContext.SaveChanges();
                }
                dbContext.Characters.Remove(foundToDelete);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Not found character!");
            }
        }

        public IEnumerable<CharacterModelDatabase> Read(string searchString=null, int? skip = null, int? limit = null)
        {
            var characters = dbContext.Characters.Include(e => e.Episodes).ThenInclude(ee => ee.Episode)
                                                 .Include(f => f.Friends).ThenInclude(ff => ff.Friend).AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                characters = characters.Where(c => c.Name.Contains(searchString));
            }

            if (skip != null)
            {
                characters = characters.Skip(skip.Value);
            }

            if (limit != null)
            {
                characters = characters.Take(limit.Value);
            }

            return characters.AsEnumerable();
        }

        public void Update(CharacterModelDatabase item)
        {
            var foundToDelete = dbContext.Characters.FirstOrDefault(c => c.Id == item.Id);
            if (foundToDelete != null)
            {
                dbContext.Characters.Update(item);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Not found character!");
            }
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

        public void CreateCharacterFriends(int characterId, List<CharacterModelDatabase> friends)
        {
            var characterFriends = new List<CharacterFriendModelDatabase>(friends.Select(f => new CharacterFriendModelDatabase() { CharacterId = characterId, FriendId = f.Id }));
            CreateCharacterFriends(characterFriends);
        }

        public void CreateCharacterFriends(List<CharacterFriendModelDatabase> characterFriends)
        {
            dbContext.CharacterFriends.AddRange(characterFriends);
            dbContext.SaveChanges();
        }

        public void CreateCharacterEpisodes(int characterId, List<EpisodeModelDatabase> episodes)
        {
            var characterEpisodes = new List<CharacterEpisodeModelDatabase>(episodes.Select(e => new CharacterEpisodeModelDatabase() {CharacterId = characterId, EpisodeId = e.Id}));
            CreateCharacterEpisodes(characterEpisodes);
        }

        public void CreateCharacterEpisodes(List<CharacterEpisodeModelDatabase> characterEpisodes)
        {
            dbContext.CharacterEpisodes.AddRange(characterEpisodes);
            dbContext.SaveChanges();
        }
    }
}
