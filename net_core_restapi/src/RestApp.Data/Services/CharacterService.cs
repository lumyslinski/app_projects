using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestApp.Data.Contracts;
using RestApp.Data.Models;

namespace RestApp.Data.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository characterRepository;
        private readonly IEpisodeRepository episodeRepository;
        private readonly IFriendRepository friendRepository;

        public CharacterService(ICharacterRepository characterRepository, IEpisodeRepository episodeRepository, IFriendRepository friendRepository)
        {
            this.characterRepository = characterRepository;
            this.episodeRepository = episodeRepository;
            this.friendRepository = friendRepository;
        }

        public CharacterServiceResult Create(ICharacterModel item)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                CharacterModelDatabase result = new CharacterModelDatabase()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                var episodes = this.episodeRepository.Read();
                result.Episodes = new List<EpisodeModelDatabase>();
                foreach (var itemEpisode in item.Episodes)
                {
                    if (episodes != null)
                    {
                        var foundEpisode = episodes.FirstOrDefault(e => e.Name == itemEpisode);
                        if (foundEpisode != null)
                        {
                            result.Episodes.Add(foundEpisode);
                        }
                        else
                        {
                            episodeRepository.Create(new EpisodeModelDatabase() { Name = itemEpisode });
                        }
                    }
                }
                var friends = this.friendRepository.Read();
                result.Friends = new List<FriendModelDatabase>();
                foreach (var itemFriend in item.Friends)
                {
                    if (friends != null)
                    {
                        var foundFriend = friends.FirstOrDefault(e => e.Name == itemFriend);
                        if (foundFriend != null)
                        {
                            result.Friends.Add(foundFriend);
                        }
                        else
                        {
                            friendRepository.Create(new FriendModelDatabase() { Name = itemFriend });
                        }
                    }
                }
                characterServiceResult.ResultStatus = characterRepository.Create(result);
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }

        public CharacterServiceResult Delete(int id)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                this.characterRepository.Delete(id);
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }

        public IEnumerable<ICharacterModel> Read()
        {
            IEnumerable<ICharacterModel> result=null;
            return result;
        }

        public CharacterServiceResult Update(ICharacterModel item)
        {
            var characterServiceResult = new CharacterServiceResult();
            try
            {
                this.characterRepository.Update();
            }
            catch (Exception exp)
            {
                characterServiceResult.Error = exp.Message;
            }

            return characterServiceResult;
        }
    }
}
