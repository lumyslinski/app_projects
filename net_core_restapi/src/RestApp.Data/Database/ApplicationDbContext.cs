using Microsoft.EntityFrameworkCore;
using RestApp.Data.Models;

namespace RestApp.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        #region Properties
        public DbSet<CharacterModelDatabase> Characters { get; set; }
        public DbSet<EpisodeModelDatabase> Episodes { get; set; }
        public DbSet<FriendModelDatabase> Friends { get; set; }
        #endregion Properties

        #region Constructor
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        #endregion Constructor

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CharacterModelDatabase>().ToTable("Character");
            modelBuilder.Entity<CharacterModelDatabase>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CharacterModelDatabase>().HasMany(i => i.Episodes).WithOne(i => i.Character);
            modelBuilder.Entity<CharacterModelDatabase>().HasMany(i => i.Friends).WithOne(i => i.Character);

            modelBuilder.Entity<EpisodeModelDatabase>().ToTable("Episode");
            modelBuilder.Entity<EpisodeModelDatabase>().Property(i => i.Id).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<FriendModelDatabase>().ToTable("Friend");
            modelBuilder.Entity<FriendModelDatabase>().Property(i => i.Id).ValueGeneratedOnAdd();
        }
        #endregion Methods
    }
}
