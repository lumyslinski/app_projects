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
            //try
            //{
            //    CharacterModelDatabase result = new CharacterModelDatabase()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //    };
            //    var episodes = this.episodeRepository.Read().ToList();
            //    result.episodes = new List<EpisodeModelDatabase>();
            //    foreach (var itemEpisode in item.episodes)
            //    {
            //        if (episodes != null)
            //        {
            //            var foundEpisode = episodes.FirstOrDefault(e => e.Name == itemEpisode);
            //            if (foundEpisode == null)
            //            {
            //                foundEpisode = new EpisodeModelDatabase() {Name = itemEpisode};
            //                var id = episodeRepository.Create(foundEpisode);
            //                foundEpisode.Id = id;
            //            }
            //            result.episodes.Add(foundEpisode);
            //        }
            //    }
            //    var friends = this.friendRepository.Read().ToList();
            //    result.Friends = new List<FriendModelDatabase>();
            //    foreach (var itemFriend in item.Friends)
            //    {
            //        if (friends != null)
            //        {
            //            var foundFriend = friends.FirstOrDefault(e => e.Name == itemFriend);
            //            if (foundFriend == null)
            //            {
            //                foundFriend = new FriendModelDatabase() { Name = itemFriend };
            //                var id = friendRepository.Create(foundFriend);
            //                foundFriend.Id = id;
            //            }
            //            result.Friends.Add(foundFriend);
            //        }
            //    }
            //    characterServiceResult.ResultId = characterRepository.Create(result);
            //}
            //catch (Exception exp)
            //{
            //    characterServiceResult.Error = exp.Message;
            //}

            return characterServiceResult;
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

        public IEnumerable<ICharacterModel> Read()
        {
            var resultFromDb = this.characterRepository.Read().ToList();
            List<ICharacterModel> result = new List<ICharacterModel>(resultFromDb.Count());
            foreach (var characterModelDatabase in resultFromDb)
            {
                var newItem = new CharacterModelBase() {Id = characterModelDatabase.Id, Name = characterModelDatabase.Name};
                newItem.Episodes = characterModelDatabase.Episodes.Where(e => e.Episode != null).Select(e => e.Episode.Name).ToList();
                newItem.Friends = characterModelDatabase.Friends.Where(f => f.Friend != null).Select(f => f.Friend.Name).ToList();
                result.Add(newItem);
            }
            return result;
        }

        public CharacterServiceResult Update(ICharacterModel item)
        {
            var characterServiceResult = new CharacterServiceResult();
            //try
            //{
            //    var characters = characterRepository.Read().ToList();
            //    var foundObject = characters.FirstOrDefault(c => c.Id == item.Id);
            //    if (foundObject != null)
            //    {
            //        if (item.Name != foundObject.Name)
            //        {
            //            foundObject.Name = item.Name;
            //        }
            //        #region episodes
            //        foreach (var itemEpisode in item.episodes)
            //        {
            //            var foundEpisode = foundObject.episodes.FirstOrDefault(e => e.Name == itemEpisode);
            //            if (foundEpisode == null)
            //            {
            //                var newEpisode = new EpisodeModelDatabase() {Name = itemEpisode};
            //                var id = episodeRepository.Create(newEpisode);
            //                newEpisode.Id = id;
            //                foundObject.episodes.Add(newEpisode);
            //            }
            //        }
            //        //check if any episode is outdated, if so then check if any character using it, if not then delete it
            //        List<EpisodeModelDatabase> episodesToRemove = new List<EpisodeModelDatabase>();
            //        foreach (var episodeModelDatabase in foundObject.episodes)
            //        {
            //            var foundEpisode = item.episodes.FirstOrDefault(e => e == episodeModelDatabase.Name);
            //            if (foundEpisode == null)
            //            {
            //                episodesToRemove.Add(episodeModelDatabase);
            //            }
            //        }
            //        foreach (var episodeModelDatabase in episodesToRemove)
            //        {
            //            foundObject.episodes.Remove(episodeModelDatabase);
            //        }
            //        // check if all episodes are used in characters, if not then delete them
            //        #endregion
            //        #region friends
            //        foreach (var itemFriend in item.Friends)
            //        {
            //            var foundEpisode = foundObject.Friends.FirstOrDefault(e => e.Name == itemFriend);
            //            if (foundEpisode == null)
            //            {
            //                var newFriend = new FriendModelDatabase() { Name = itemFriend };
            //                var id = friendRepository.Create(newFriend);
            //                newFriend.Id = id;
            //                foundObject.Friends.Add(newFriend);
            //            }
            //        }
            //        //check if any friend is outdated, if so then check if any character using it, if not then delete it
            //        List<FriendModelDatabase> friendsToRemove = new List<FriendModelDatabase>();
            //        foreach (var friendModelDatabase in foundObject.Friends)
            //        {
            //            var foundEpisode = item.episodes.FirstOrDefault(e => e == friendModelDatabase.Name);
            //            if (foundEpisode == null)
            //            {
            //                friendsToRemove.Add(friendModelDatabase);
            //            }
            //        }
            //        foreach (var friendModelDatabase in friendsToRemove)
            //        {
            //            foundObject.Friends.Remove(friendModelDatabase);
            //        }
            //        // check if all episodes are used in characters, if not then delete them
            //        #endregion
            //        characterRepository.Update(foundObject);
            //        characterServiceResult.ResultId = foundObject.Id; 
            //    }
            //    else
            //    {
            //        characterServiceResult.Error = "Object not found for update!";
            //    }
            //}
            //catch (Exception exp)
            //{
            //    characterServiceResult.Error = exp.Message;
            //}

            return characterServiceResult;
        }
    }
}
