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
    public class IntegrationTestCharacterRepository
    {
        private ApplicationDbContext GetApplicationDbContext()
        {
            try
            {
                var applicationDbContextDesignTimeDbContextFactory = new ApplicationDbContextDesignTimeDbContextFactory();
                var dbContext = applicationDbContextDesignTimeDbContextFactory.Create();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                return dbContext;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return null;
        }

        [Fact]
        public void UnitTestCharacterRepository()
        {
            IEnumerable<CharacterModelDatabase> readResult = null;
            try
            {
                var dbContext = GetApplicationDbContext();
                Assert.NotNull(dbContext);
                CharacterRepository characterRepository = new CharacterRepository(dbContext);
                readResult = characterRepository.Read();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            Assert.True(readResult.Count() == 7);
        }

        [Fact]
        public void UnitTestCharacterServiceRead()
        {
            IEnumerable<ICharacterModel> readResult = null;
            try
            {
                var dbContext = GetApplicationDbContext();
                CharacterRepository characterRepository = new CharacterRepository(dbContext);
                EpisodeRepository episodeRepository = new EpisodeRepository(dbContext);
                var service = new CharacterService(characterRepository, episodeRepository);
                readResult = service.Read();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            Assert.True(readResult.Count() == 7);
        }
    }
}
