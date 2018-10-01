using System;

namespace ImageApp.Data.Database
{
    public static class RepositoryContextProvider
    {
        public static RepositoryContext GetInstance()
        {
            try
            {
                var applicationDbContextDesignTimeDbContextFactory = new RepositoryContextDesignTimeDbContextFactory();
                var dbContext = applicationDbContextDesignTimeDbContextFactory.Create();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                return dbContext;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
