using Microsoft.EntityFrameworkCore;
using Silverscreen.Core.Indexers;
using Silverscreen.Core.Download;

namespace Silverscreen.Core.Model {
    public class MediaCollectionContext : DbContext  {
        public MediaCollectionContext (DbContextOptions<MediaCollectionContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<LibraryDirectory>()
                .HasIndex(d => d.DirectoryPath)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.ImdbId)
                .IsUnique();
        }
        public DbSet<LibraryDirectory> Directories { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}

//https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html