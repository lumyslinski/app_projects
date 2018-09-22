using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestApp.Data.Contracts;
using RestApp.Data.Database;
using RestApp.Data.Models;
using RestApp.Data.Repositories;
using RestApp.Data.Services;
using Xunit;

namespace RestApp.XUnitTests.Integration
{
    public class IntegrationTestCharacterService
    {
        [Fact]
        public void UnitTestCharacterServiceRead()
        {
            IEnumerable<ICharacterModel> readResult = null;
            try
            {
                using (var dbContext = ApplicationDbContextContainer.GetInstance())
                {
                    Assert.NotNull(dbContext);
                    CharacterRepository characterRepository = new CharacterRepository(dbContext);
                    EpisodeRepository episodeRepository = new EpisodeRepository(dbContext);
                    CharacterService characterService = new CharacterService(characterRepository, episodeRepository);
                    readResult = characterService.Read();
                    Assert.NotNull(readResult);
                    var firstCharacter = readResult.FirstOrDefault();
                    Assert.NotNull(firstCharacter);
                    Assert.True(firstCharacter.Name == "Luke Skywalker");
                    Assert.NotNull(firstCharacter);
                    Assert.NotNull(firstCharacter.Episodes);
                    var episodes = new List<string> {"NEWHOPE", "EMPIRE", "JEDI"};
                    foreach (var characterEpisode in firstCharacter.Episodes)
                    {
                        Assert.NotNull(characterEpisode);
                        Assert.Contains(episodes, f => f == characterEpisode);
                    }
                    Assert.NotNull(firstCharacter.Friends);
                    var friends = new List<string> {"Han Solo", "Leia Organa", "C-3PO", "R2-D2"};
                    foreach (var characterFriend in firstCharacter.Friends)
                    {
                        Assert.NotNull(characterFriend);
                        Assert.Contains(friends, f => f == characterFriend);
                    }
                }
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
               readResult = null;
            }

            //only on creating
            //Assert.True(readResult.Count() == 7);
        }

        [Fact]
        public void UnitTestCharacterServiceCreate()
        {
            try
            {
                using (var dbContext = ApplicationDbContextContainer.GetInstance())
                {
                    Assert.NotNull(dbContext);
                    CharacterRepository characterRepository = new CharacterRepository(dbContext);
                    EpisodeRepository episodeRepository = new EpisodeRepository(dbContext);
                    CharacterService characterService = new CharacterService(characterRepository, episodeRepository);
                    var getTestItem = characterService.GetItemByName("Kylo");
                    var newId = 0;
                    if (getTestItem == null)
                    {
                        var createResult = characterService.Create(new CharacterModelBase()
                        {
                            Name = "Kylo",
                            Episodes = new List<string>() {"NEWHOPE", "EMPIRE", "JEDI", "FORCE"},
                            Friends = new List<string>() {"Darth Vader", "Rey"}
                        });
                        Assert.True(createResult.ResultIsOk);
                        newId = createResult.ResultId;
                    }
                    else
                    {
                        newId = getTestItem.Id;
                    }
                    var deleteResult = characterService.Delete(newId);
                    Assert.True(deleteResult.ResultIsOk);
                    var getItem = characterService.GetItemById(newId);
                    Assert.Null(getItem);
                }
            }
            catch (Exception exp)
            {
                throw;
            }  
        }

        [Fact]
        public void UnitTestCharacterServiceUpdate()
        {
            try
            {
                using (var dbContext = ApplicationDbContextContainer.GetInstance())
                {
                    Assert.NotNull(dbContext);
                    CharacterRepository characterRepository = new CharacterRepository(dbContext);
                    EpisodeRepository episodeRepository = new EpisodeRepository(dbContext);
                    CharacterService characterService = new CharacterService(characterRepository, episodeRepository);
                    var getTestItem = characterService.GetItemByName("Kylo");
                    var newId = 0;
                    if (getTestItem == null)
                    {
                        getTestItem = new CharacterModelBase()
                        {
                            Name = "Kylo",
                            Episodes = new List<string>() { "NEWHOPE", "EMPIRE", "JEDI", "FORCE" },
                            Friends = new List<string>() { "Darth Vader", "Rey" }
                        };
                        var createResult = characterService.Create(getTestItem);
                        Assert.True(createResult.ResultIsOk);
                        newId = createResult.ResultId;
                    }
                    else
                    {
                        newId = getTestItem.Id;
                    }
                    getTestItem.Id = newId;
                    getTestItem.Name = "Kyle";
                    getTestItem.Episodes = new List<string>() {"NEWHOPE", "JEDI", "FORCE"};
                    getTestItem.Friends = new List<string>() {"Rey"};
                    var updateResult = characterService.Update(getTestItem);
                    Assert.True(updateResult.ResultIsOk);
                    var getItem = characterService.GetItemById(newId);
                    Assert.True(getTestItem.Episodes.Count() == 3);
                    Assert.True(getTestItem.Friends.Count() == 1);
                    characterService.Delete(newId);
                    getItem = characterService.GetItemById(newId);
                    Assert.Null(getItem);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
        }
    }
}
