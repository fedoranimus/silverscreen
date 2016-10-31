using Microsoft.EntityFrameworkCore;

namespace Silverscreen.Model {
    public class LibraryContext : DbContext  {
        public LibraryContext (DbContextOptions<LibraryContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Directory>()
                .HasIndex(d => d.DirectoryPath)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.ImdbId)
                .IsUnique();
        }
        public DbSet<Directory> Directories { get; set; }
        public DbSet<Movie> Movies { get; set;}
    }
}

//https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html