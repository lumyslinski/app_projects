using System;
using Microsoft.EntityFrameworkCore;

namespace ImageApp.Data.Database
{
    public class RepositoryContextDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<RepositoryContext>
    {
        protected override RepositoryContext CreateNewInstance(DbContextOptions<RepositoryContext> options)
        {
            return new RepositoryContext(options);
        }
    }
}
