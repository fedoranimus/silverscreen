using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Silverscreen.Library;
using Silverscreen.Model;
using Xunit;
using System;

namespace Silverscreen.Library.Tests {

    public class LibraryServiceTests {

        private static DbContextOptions<LibraryContext> CreateNewContextOptions() {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<LibraryContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

           return builder.Options;
        }

        [Fact]
        public async void Add_Directory_To_Library()
        {
            var options = CreateNewContextOptions();

            using(var context = new LibraryContext(options))
            {
                var service = new LibraryService(context);
                var directory = await service.AddDirectory(@"\\Plex\Movies");
            }

            using(var context = new LibraryContext(options))
            {
                Assert.Equal(context.Directories.FirstOrDefault().DirectoryPath, "\\\\Plex\\Movies"); //ensure we're passing the correct string
            }
        }

        [Fact]
        public void Scan_Library() 
        {
            var options = CreateNewContextOptions();

            using(var context = new LibraryContext(options))
            {
                var service = new LibraryService(context);
                service.ScanLibrary();
            }

            using(var context = new LibraryContext(options))
            {
                Assert.Equal(context.Movies.Count(), 295);
            }
        }
    }
}