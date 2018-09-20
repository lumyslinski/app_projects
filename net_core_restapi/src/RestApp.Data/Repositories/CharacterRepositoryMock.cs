using System.Collections.Generic;
using RestApp.Data.Contracts;

namespace RestApp.Data.Repositories
{
    public class CharacterRepositoryMock: IGenericRepository<ICharacterModel>
    {
        List<ICharacterModel> characters;

        public CharacterRepositoryMock()
        {
            characters = new List<ICharacterModel>();
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 0,
            //    Name = "Luke Skywalker",
            //    Episodes = new List<EpisodeModelDatabase>() {"NEWHOPE", "EMPIRE", "JEDI"},
            //    Friends = new List<string>() {"Han Solo", "Leia Organa", "C-3PO", "R2-D2"}
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 1,
            //    Name = "Darth Vader",
            //    Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI" },
            //    Friends = new List<string>() { "Wilhuff Tarkin" }
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 2,
            //    Name = "Han Solo",
            //    Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI" },
            //    Friends = new List<string>() { "Luke Skywalker", "Leia Organa", "R2-D2" }
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 3,
            //    Name = "Leia Organa",
            //    Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI" },
            //    Friends = new List<string>() { "Luke Skywalker", "Han Solo", "C-3PO", "R2-D2" }
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 4,
            //    Name = "Wilhuff Tarkin",
            //    Episodes = new List<string>() { "NEWHOPE" },
            //    Friends = new List<string>() { "Darth Vader" }
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 5,
            //    Name = "C-3PO",
            //    Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI" },
            //    Friends = new List<string>() { "Luke Skywalker", "Han Solo", "Leia Organa", "R2-D2" }
            //});
            //result.Add(new CharacterModelDatabase()
            //{
            //    Id = 6,
            //    Name = "R2-D2",
            //    Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI" },
            //    Friends = new List<string>() { "Luke Skywalker", "Han Solo", "Leia Organa" }
            //});
        }

        public void Create(ICharacterModel character)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int characterId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICharacterModel> Read()
        {
            throw new System.NotImplementedException();
        }

        public void Update(ICharacterModel characterId)
        {
            throw new System.NotImplementedException();
        }
    }
}
