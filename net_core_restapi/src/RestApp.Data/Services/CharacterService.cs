using System;
using System.Collections.Generic;
using System.Linq;
using RestApp.Data.Contracts;
using RestApp.Data.Models;

namespace RestApp.Data.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IEpisodeRepository episodeRepository;

        public CharacterService(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository)
        {
            this.characterRepository = characterRepository;
            this.episodeRepository = episodeRepository;
        }

        public CharacterServiceResult Create(ICharacterModel item)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                var newCharacterModelDatabase = new CharacterModelDatabase()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                var newCharacterModelDatabaseId = this.characterRepository.Create(newCharacterModelDatabase);
                if (item.Episodes != null && item.Episodes.Any())
                {
                    var episodesDatabase = this.episodeRepository.Read().ToList();
                    var newEpisodeModels = AddNewCharacterEpisodes(item.Episodes, episodesDatabase, newCharacterModelDatabaseId);
                    newCharacterModelDatabase.Episodes = newEpisodeModels;
                }

                if (item.Friends != null && item.Friends.Any())
                {
                    var friendsDatabase = this.characterRepository.Read().ToList();
                    var newFriendModels = AddNewCharacterFriends(item.Friends, friendsDatabase, newCharacterModelDatabaseId);
                    newCharacterModelDatabase.Friends = newFriendModels;
                }
                this.characterRepository.Update(newCharacterModelDatabase);
                characterServiceResult.ResultId = newCharacterModelDatabaseId;
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }

        private List<CharacterFriendModelDatabase> AddNewCharacterFriends(List<string> friendsItem, List<CharacterModelDatabase> friendsDatabase, int newCharacterModelDatabaseId)
        {
            var newFriendModels = new List<CharacterFriendModelDatabase>();
            foreach (var itemFriend in friendsItem)
            {
                var foundFriend = friendsDatabase.FirstOrDefault(e => e.Name == itemFriend);
                if (foundFriend == null)
                {
                    foundFriend = new CharacterModelDatabase() {Name = itemFriend};
                    this.characterRepository.Create(foundFriend);
                }

                newFriendModels.Add(new CharacterFriendModelDatabase()
                {
                    CharacterId = newCharacterModelDatabaseId,
                    FriendId = foundFriend.Id
                });
            }

            return newFriendModels;
        }

        private List<CharacterEpisodeModelDatabase> AddNewCharacterEpisodes(List<string> episodesItem, List<EpisodeModelDatabase> episodesDatabase, int newCharacterModelDatabaseId)
        {
            var newEpisodeModels = new List<CharacterEpisodeModelDatabase>();
            foreach (var itemEpisode in episodesItem)
            {
                var foundEpisode = episodesDatabase.FirstOrDefault(e => e.Name == itemEpisode);
                if (foundEpisode == null)
                {
                    foundEpisode = new EpisodeModelDatabase() {Name = itemEpisode};
                    episodeRepository.Create(foundEpisode);
                }

                newEpisodeModels.Add(new CharacterEpisodeModelDatabase()
                {
                    CharacterId = newCharacterModelDatabaseId,
                    EpisodeId = foundEpisode.Id
                });
            }

            return newEpisodeModels;
        }

        public CharacterServiceResult Delete(int id)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                this.characterRepository.Delete(id);
                characterServiceResult.ResultId = id;
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }

        public ICharacterModel GetItemByName(string name)
        {
            var resultFromDb = this.characterRepository.Read().ToList();
            CharacterModelBase result = null;
            foreach (var characterModelDatabase in resultFromDb)
            {
                if (characterModelDatabase.Name == name)
                {
                    result = ConvertToCharacterModelBase(characterModelDatabase);
                    break;
                }
            }
            return result;
        }

        public ICharacterModel GetItemById(int id)
        {
            var resultFromDb = this.characterRepository.Read().ToList();
            CharacterModelBase result = null;
            foreach (var characterModelDatabase in resultFromDb)
            {
                if (characterModelDatabase.Id == id)
                {
                    result = ConvertToCharacterModelBase(characterModelDatabase);
                    break;
                }
            }
            return result;
        }

        public IEnumerable<ICharacterModel> Read()
        {
            var resultFromDb = this.characterRepository.Read().ToList();
            List<ICharacterModel> result = new List<ICharacterModel>(resultFromDb.Count());
            foreach (var characterModelDatabase in resultFromDb)
            {
                var newItem = ConvertToCharacterModelBase(characterModelDatabase);
                result.Add(newItem);
            }
            return result;
        }

        private static CharacterModelBase ConvertToCharacterModelBase(CharacterModelDatabase characterModelDatabase)
        {
            var newItem = new CharacterModelBase() {Id = characterModelDatabase.Id, Name = characterModelDatabase.Name};
            newItem.Episodes = characterModelDatabase.Episodes.Where(e => e.Episode != null).Select(e => e.Episode.Name).ToList();
            newItem.Friends = characterModelDatabase.Friends.Where(f => f.Friend != null).Select(f => f.Friend.Name).ToList();
            return newItem;
        }

        public CharacterServiceResult Update(ICharacterModel item)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                var characters = characterRepository.Read().ToList();
                var foundObject = characters.FirstOrDefault(c => c.Id == item.Id);
                if (foundObject != null)
                {
                    if (item.Name != foundObject.Name)
                    {
                        foundObject.Name = item.Name;
                    }
                    #region episodes 
                    //check if any episode is outdated, if so then check if any character using it, if not then delete it
                    List<CharacterEpisodeModelDatabase> episodesToRemove = new List<CharacterEpisodeModelDatabase>();
                    foreach (var episodeModelDatabase in foundObject.Episodes)
                    {
                        var foundEpisode = item.Episodes.FirstOrDefault(e => e == episodeModelDatabase.Episode.Name);
                        if (foundEpisode == null) episodesToRemove.Add(episodeModelDatabase);
                    }
                    foreach (var episodeModelDatabase in episodesToRemove)
                    {
                        foundObject.Episodes.Remove(episodeModelDatabase);
                        this.characterRepository.DeleteCharacterEpisode(episodeModelDatabase);
                    }
                    var episodesDatabase = this.episodeRepository.Read().ToList();
                    var newEpisodeModels = AddNewCharacterEpisodes(item.Episodes, episodesDatabase, foundObject.Id);
                    foundObject.Episodes = newEpisodeModels;
                    // check if all episodes are used in characters, if not then delete them
                    #endregion
                    #region friends
                    //check if any friend is outdated, if so then check if any character using it, if not then delete it
                    List<CharacterFriendModelDatabase> friendsToRemove = new List<CharacterFriendModelDatabase>();
                    foreach (var friendModelDatabase in foundObject.Friends)
                    {
                        var foundFriend = item.Friends.FirstOrDefault(e => e == friendModelDatabase.Friend.Name);
                        if (foundFriend == null) friendsToRemove.Add(friendModelDatabase);
                    }
                    foreach (var friendModelDatabase in friendsToRemove)
                    {
                        foundObject.Friends.Remove(friendModelDatabase);
                        this.characterRepository.DeleteCharacterFriend(friendModelDatabase);
                    }
                    var friendsDatabase = this.characterRepository.Read().ToList();
                    var newFriendModels = AddNewCharacterFriends(item.Friends, friendsDatabase, foundObject.Id);
                    foundObject.Friends = newFriendModels;
                    // check if all episodes are used in characters, if not then delete them
                    #endregion
                    characterRepository.Update(foundObject);
                    characterServiceResult.ResultId = foundObject.Id;
                }
                else
                {
                    characterServiceResult.Error = "Object not found for update!";
                }
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }
    }
}
