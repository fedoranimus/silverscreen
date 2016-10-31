using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Silverscreen.Library;
using Silverscreen.Model;
using Xunit;
using System;

namespace Silverscreen.Library.Tests
{
    public class ParsingTests {
        private DbContextOptions<LibraryContext> _contextOptions;

        public ParsingTests() {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<LibraryContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

            _contextOptions = builder.Options;
            
        }

        [Fact]    
        public void ParseDirectoryWithThe()
        {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                string movie = @"Legend of Tarzan, The (2016)";
                MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

                Assert.Equal(movieTitle.Title, "Legend of Tarzan");
            }
        }

        [Fact]     
        public void ParseDirectoryWithTheYear()
        {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                string movie = @"Legend of Tarzan, The (2016)";
                MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

                Assert.Equal(movieTitle.Year, "2016");
            }
        }

        [Fact]
        public void ParseDirectorySimple()
        {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                string movie = @"Imperium (2016)";
                MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

                Assert.Equal(movieTitle.Title, "Imperium");
            }
        }

        [Fact]
        public void ParseDirectorySimpleYear()
        {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                string movie = @"Imperium (2016)";
                MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

                Assert.Equal(movieTitle.Year, "2016");
            }
        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Crouching Tiger, Hidden Dragon Sword of Destiny (2016)")]
        public void ParseDirectoryRealDataTitle1(string directory) {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                MovieTitle movieTitle = _libraryService.FindVideo(directory);

                Assert.Equal(movieTitle.Title, "Crouching Tiger, Hidden Dragon Sword of Destiny");
            }

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\10 Cloverfield Lane (2016)")]
        public void ParseDirectoryRealDataTitle2(string directory) {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                MovieTitle movieTitle = _libraryService.FindVideo(directory);

                Assert.Equal(movieTitle.Title, "10 Cloverfield Lane");
            }

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Dukes of Hazzard, The (2005)")]
        public void ParseDirectoryRealDataTitle3(string directory) {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                MovieTitle movieTitle = _libraryService.FindVideo(directory);

                Assert.Equal(movieTitle.Title, "The Dukes of Hazzard");
            }

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\10 Cloverfield Lane (2016)")]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Crouching Tiger, Hidden Dragon Sword of Destiny (2016)")]
        public void ParseDirectoryRealDataYear2016(string directory) {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                MovieTitle movieTitle = _libraryService.FindVideo(directory);

                Assert.Equal(movieTitle.Year, "2016");
            }

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Dukes of Hazzard, The (2005)")]
        public void ParseDirectoryRealDataYear2005(string directory) {
            using(var context = new LibraryContext(_contextOptions)) {
                var _libraryService = new LibraryService(context);
                MovieTitle movieTitle = _libraryService.FindVideo(directory);

                Assert.Equal(movieTitle.Year, "");
            }
        }
    }
}