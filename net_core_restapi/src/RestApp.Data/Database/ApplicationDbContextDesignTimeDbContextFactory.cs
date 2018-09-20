using Microsoft.EntityFrameworkCore;

namespace RestApp.Data.Database
{
    public class ApplicationDbContextDesignTimeDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
