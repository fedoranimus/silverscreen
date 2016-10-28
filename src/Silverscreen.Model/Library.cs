using Microsoft.EntityFrameworkCore;

namespace Silverscreen.Model {
    public class LibraryContext : DbContext, IEntityBase  {
        public LibraryContext (DbContextOptions<LibraryContext> options) : base(options) 
        {

        }


        public int Id { get; set; }
        public DbSet<string> Directories { get; set; }
        public DbSet<Movie> Movies { get; set;}
    }
}

//https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html