using Microsoft.EntityFrameworkCore;
using Silverscreen.Core.Indexers;
using Silverscreen.Core.Download;

namespace Silverscreen.Core.Model {
    public class SettingsContext : DbContext  {
        public SettingsContext (DbContextOptions<SettingsContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Indexer>()
                .HasIndex(i => i.Name)
                .IsUnique();

            modelBuilder.Entity<DownloadClientDefinition>()
                .HasIndex(d => d.Name)
                .IsUnique();
        }
        public DbSet<Indexer> Indexers { get; set; }

        public DbSet<DownloadClientDefinition> DownloadClientDefinitions { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<QualityDefinition> QualityDefinitions { get; set; }

    }
}

//https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html