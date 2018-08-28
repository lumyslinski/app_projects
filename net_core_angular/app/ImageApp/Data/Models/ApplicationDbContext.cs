using Microsoft.EntityFrameworkCore;

namespace ImageApp.Data.Models
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions options): base(options) { }
        #endregion Constructor

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ImageModelDb>().ToTable("ImageModel");
            modelBuilder.Entity<ImageModelDb>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ImageModelDb>().HasMany(i => i.ImageModelDetails).WithOne(i => i.Image);
            modelBuilder.Entity<ImageModelDb>().HasMany(i => i.ImageModelDetailsWeb).WithOne(i => i.Image);
            modelBuilder.Entity<ImageModelDb>().HasMany(i => i.ImageModelWebMatches).WithOne(i => i.Image);
            modelBuilder.Entity<ImageModelDetailsDb>().ToTable("ImageModelDetails");
            modelBuilder.Entity<ImageModelDetailsDb>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ImageModelDetailsDb>().HasOne(i => i.Image).WithMany(d => d.ImageModelDetails);
            modelBuilder.Entity<ImageModelDetailsWebDb>().ToTable("ImageModelDetailsWeb");
            modelBuilder.Entity<ImageModelDetailsWebDb>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ImageModelDetailsWebDb>().HasOne(i => i.Image).WithMany(d => d.ImageModelDetailsWeb);
            modelBuilder.Entity<ImageModelWebMatchesDb>().ToTable("ImageModelWebMatches");
            modelBuilder.Entity<ImageModelWebMatchesDb>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ImageModelWebMatchesDb>().HasOne(i => i.Image).WithMany(d => d.ImageModelWebMatches);
        }
        #endregion Methods

        #region Properties
        public DbSet<ImageModelDetailsWebDb> ImageModelDetailsWeb { get; set; }
        public DbSet<ImageModelWebMatchesDb> ImageModelWebMatches { get; set; }
        public DbSet<ImageModelDetailsDb> ImageModelDetails { get; set; }
        public DbSet<ImageModelDb> Images { get; set; }
        #endregion Properties
    }
}