using System.Collections.Generic;
using RestApp.Data.Models;

namespace RestApp.Data.Contracts
{
    public interface ICharacterService
    {
        CharacterServiceResult Create(ICharacterModel item);
        IEnumerable<ICharacterModel> Read();
        CharacterServiceResult Update(ICharacterModel item);
        CharacterServiceResult Delete(int id);
    }
}
