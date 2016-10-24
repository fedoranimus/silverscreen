using Xunit;
using Silverscreen.Library;

namespace Silverscreen.Library.Tests
{
    public class ParsingTests {
        private readonly LibraryManager _libraryManager;

        public ParsingTests() {
            _libraryManager = new LibraryManager();
        }

        [Fact]
        [InlineData()]        
        public void ParseDirectoryWithThe()
        {
            string directory = @"\\Plex\Movies\Legend of Tarzan, The (2016)";
            MovieTitle movieTitle = _libraryManager.ParseMovieDirectoryName(directory);

            Assert.Equal(movieTitle.Title, "Legend of Tarzan");
        }

        [Fact]
        [InlineData()]        
        public void ParseDirectoryWithTheYear()
        {
            string directory = @"\\Plex\Movies\Legend of Tarzan, The (2016)";
            MovieTitle movieTitle = _libraryManager.ParseMovieDirectoryName(directory);

            Assert.Equal(movieTitle.Year, "2016");
        }

        [Fact]
        public void ParseDirectorySimple()
        {
            string directory = @"\\Plex\Movies\Imperium (2016)";
            MovieTitle movieTitle = _libraryManager.ParseMovieDirectoryName(directory);

            Assert.Equal(movieTitle.Title, "Imperium");
        }

        [Fact]
        public void ParseDirectorySimpleYear()
        {
            string directory = @"\\Plex\Movies\Imperium (2016)";
            MovieTitle movieTitle = _libraryManager.ParseMovieDirectoryName(directory);

            Assert.Equal(movieTitle.Year, "2016");
        }
    }
}