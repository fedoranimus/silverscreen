using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Silverscreen.Core.Library;
using Silverscreen.Core.Model;
using Silverscreen.Core.Parser;
using Xunit;
using System;
using System.Net;

namespace Silverscreen.Library.Tests {

    public class LibraryServiceTests {
        public LibraryServiceTests() {
            NetworkCredential credentials = new NetworkCredential("admin", "");
            CredentialCache netCache = new CredentialCache();
            netCache.Add(new Uri(@"\\Plex"), "Digest", credentials);
            Console.WriteLine("Added Credentials");
        }

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
                var parser = new ParserService();
                var service = new LibraryService(context, parser);
                var directory = await service.AddDirectory(@"\\Plex\Movies");
                Assert.Equal(context.Directories.FirstOrDefault().DirectoryPath, "\\\\Plex\\Movies"); //ensure we're passing the correct string
            }
        }

        [Fact]
        public async void Scan_Library() 
        {
            var options = CreateNewContextOptions();

            using(var context = new LibraryContext(options))
            {
                var parser = new ParserService();
                var service = new LibraryService(context, parser);
                await service.AddDirectory(@"\\Plex\Movies");
                await service.ScanLibrary();
                Console.WriteLine("Found {0} movies.", context.Movies.Count());
                Assert.Equal(context.Movies.Count(), 10); //Don't expect anything to work yet
            }
        }

        [Fact]
        public async void Scan_Subset()
        {
            var options = CreateNewContextOptions();

            using (var context = new LibraryContext(options))
            {
                var parser = new ParserService();
                var service = new LibraryService(context, parser);
                await service.AddDirectory(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\TestData\Videos");
                await service.ScanLibrary();
                Assert.Equal(context.Movies.Count(), 3);
            }
        }
    }
}