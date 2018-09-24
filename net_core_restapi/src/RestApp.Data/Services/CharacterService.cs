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
                var newCharacterModelDatabase = new CharacterModelDatabase() { Id = item.Id, Name = item.Name };
                var newCharacterModelDatabaseId = this.characterRepository.Create(newCharacterModelDatabase);
                if (item.Episodes != null && item.Episodes.Any())
                {
                    var addedResult = AddNewCharacterEpisodes(newCharacterModelDatabaseId, item.Episodes);
                    this.characterRepository.CreateCharacterEpisodes(addedResult.CharacterEpisodes);
                }
                if (item.Friends != null && item.Friends.Any())
                {
                    var addedResult = AddNewCharacterFriends(newCharacterModelDatabaseId, item.Friends);
                    this.characterRepository.CreateCharacterFriends(addedResult.CharacterFriends);
                }
                characterServiceResult.ResultId = newCharacterModelDatabaseId;
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }

        private CharacterFriendModelDatabaseChangedState AddNewCharacterFriends(int characterId, List<string> friendsItem)
        {
            var result = new CharacterFriendModelDatabaseChangedState();
            var friendsDatabase = this.characterRepository.Read().ToList();
            foreach (var itemFriend in friendsItem)
            {
                var foundFriend = friendsDatabase.FirstOrDefault(e => e.Name == itemFriend);
                if (foundFriend == null)
                {
                    foundFriend = new CharacterModelDatabase() {Name = itemFriend};
                    this.characterRepository.Create(foundFriend);
                    result.CharacterFriendsChanged.Add(new CharacterFriendModelDatabase() { CharacterId = characterId, FriendId = foundFriend.Id, Friend = foundFriend });
                }
                result.CharacterFriends.Add(new CharacterFriendModelDatabase() { CharacterId = characterId, FriendId = foundFriend.Id, Friend = foundFriend });
            }
            return result;
        }

        private CharacterEpisodeModelDatabaseChangedState AddNewCharacterEpisodes(int characterId, List<string> episodesItem)
        {
            var resultCharacterEpisodes = new CharacterEpisodeModelDatabaseChangedState();
            var episodesDatabase = this.episodeRepository.Read().ToList();
            foreach (var itemEpisode in episodesItem)
            {
                var foundEpisode = episodesDatabase.FirstOrDefault(e => e.Name == itemEpisode);
                if (foundEpisode == null)
                {
                    foundEpisode = new EpisodeModelDatabase() { Name = itemEpisode };
                    this.episodeRepository.Create(foundEpisode);
                    resultCharacterEpisodes.CharacterEpisodesChanged.Add(new CharacterEpisodeModelDatabase() { CharacterId = characterId, EpisodeId = foundEpisode.Id, Episode = foundEpisode });
                }
                resultCharacterEpisodes.CharacterEpisodes.Add(new CharacterEpisodeModelDatabase(){ CharacterId = characterId, EpisodeId = foundEpisode.Id, Episode = foundEpisode });
            }
            return resultCharacterEpisodes;
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

        public IEnumerable<ICharacterModel> Read(string searchString = null, int? skip = null, int? limit = null)
        {
            var resultFromDb = this.characterRepository.Read(searchString,skip,limit).ToList();
            List<ICharacterModel> result = new List<ICharacterModel>(resultFromDb.Count());
            foreach (var characterModelDatabase in resultFromDb)
            {
                var newItem = ConvertToCharacterModelBase(characterModelDatabase);
                result.Add(newItem);
            }
            return result;
        }

        public CharacterModelBase ConvertToCharacterModelBase(CharacterModelDatabase characterModelDatabase)
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
                var foundObject = characterRepository.GetItem(item.Id);
                if (foundObject != null)
                {
                    if (item.Name != foundObject.Name)
                    {
                        foundObject.Name = item.Name;
                        characterRepository.Update(foundObject);
                    }
                    #region episodes 
                    if (item.Episodes != null && item.Episodes.Any())
                    {
                        var updatedResult  = AddNewCharacterEpisodes(foundObject.Id, item.Episodes);
                        if(updatedResult.CharacterEpisodesChanged.Any()) this.characterRepository.CreateCharacterEpisodes(updatedResult.CharacterEpisodesChanged);
                        var episodesToRemove = new List<CharacterEpisodeModelDatabase>();
                        foreach (var episodeModelDatabase in foundObject.Episodes)
                        {
                            if (!item.Episodes.Contains(episodeModelDatabase.Episode.Name)) episodesToRemove.Add(episodeModelDatabase);
                        }
                        foreach (var episodeModelDatabase in episodesToRemove)
                        {
                            foundObject.Episodes.Remove(episodeModelDatabase);
                            this.characterRepository.DeleteCharacterEpisode(episodeModelDatabase);
                        }
                    }
                    #endregion
                    #region friends
                    if (item.Friends != null && item.Friends.Any())
                    {
                        var updatedResult = AddNewCharacterFriends(foundObject.Id, item.Friends);
                        if (updatedResult.CharacterFriendsChanged.Any()) this.characterRepository.CreateCharacterFriends(updatedResult.CharacterFriendsChanged);
                        List<CharacterFriendModelDatabase> friendsToRemove = new List<CharacterFriendModelDatabase>();
                        foreach (var friendModelDatabase in foundObject.Friends)
                        {
                            if (!item.Friends.Contains(friendModelDatabase.Friend.Name)) friendsToRemove.Add(friendModelDatabase);
                        }
                        foreach (var friendModelDatabase in friendsToRemove)
                        {
                            foundObject.Friends.Remove(friendModelDatabase);
                            this.characterRepository.DeleteCharacterFriend(friendModelDatabase);
                        }
                    }
                    #endregion
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
