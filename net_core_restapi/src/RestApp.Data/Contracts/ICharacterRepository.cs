using System.Collections.Generic;
using RestApp.Data.Models;

namespace RestApp.Data.Contracts
{
    public interface ICharacterRepository : IGenericRepository<CharacterModelDatabase>
    {
        void DeleteCharacterEpisode(CharacterEpisodeModelDatabase item);
        void DeleteCharacterFriend(CharacterFriendModelDatabase item);
        void CreateCharacterFriends(int characterId, List<CharacterModelDatabase> friends);
        void CreateCharacterFriends(List<CharacterFriendModelDatabase> characterFriends);
        void CreateCharacterEpisodes(int characterId, List<EpisodeModelDatabase> episodes);
        void CreateCharacterEpisodes(List<CharacterEpisodeModelDatabase> characterEpisodes);
    }
}