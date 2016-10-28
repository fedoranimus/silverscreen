using Xunit;

namespace Silverscreen.Library.Tests
{
    public class ParsingTests {
        private readonly LibraryService _libraryService;

        public ParsingTests() {
            _libraryService = new LibraryService();
        }

        [Fact]    
        public void ParseDirectoryWithThe()
        {
            string movie = @"Legend of Tarzan, The (2016)";
            MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

            Assert.Equal(movieTitle.Title, "Legend of Tarzan");
        }

        [Fact]     
        public void ParseDirectoryWithTheYear()
        {
            string movie = @"Legend of Tarzan, The (2016)";
            MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

            Assert.Equal(movieTitle.Year, "2016");
        }

        [Fact]
        public void ParseDirectorySimple()
        {
            string movie = @"Imperium (2016)";
            MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

            Assert.Equal(movieTitle.Title, "Imperium");
        }

        [Fact]
        public void ParseDirectorySimpleYear()
        {
            string movie = @"Imperium (2016)";
            MovieTitle movieTitle = _libraryService.ParseMovieName(movie);

            Assert.Equal(movieTitle.Year, "2016");
        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Crouching Tiger, Hidden Dragon Sword of Destiny (2016)")]
        public void ParseDirectoryRealDataTitle1(string directory) {
            MovieTitle movieTitle = _libraryService.FindVideo(directory);

            Assert.Equal(movieTitle.Title, "Crouching Tiger, Hidden Dragon Sword of Destiny");

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\10 Cloverfield Lane (2016)")]
        public void ParseDirectoryRealDataTitle2(string directory) {
            MovieTitle movieTitle = _libraryService.FindVideo(directory);

            Assert.Equal(movieTitle.Title, "10 Cloverfield Lane");

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Dukes of Hazzard, The (2005)")]
        public void ParseDirectoryRealDataTitle3(string directory) {
            MovieTitle movieTitle = _libraryService.FindVideo(directory);

            Assert.Equal(movieTitle.Title, "The Dukes of Hazzard");

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\10 Cloverfield Lane (2016)")]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Crouching Tiger, Hidden Dragon Sword of Destiny (2016)")]
        public void ParseDirectoryRealDataYear2016(string directory) {
            MovieTitle movieTitle = _libraryService.FindVideo(directory);

            Assert.Equal(movieTitle.Year, "2016");

        }

        [Theory]
        [InlineData(@"C:\Users\Tim\Documents\GitHub\silverscreen\test\Silverscreen.Library.Tests\TestData\Videos\Dukes of Hazzard, The (2005)")]
        public void ParseDirectoryRealDataYear2005(string directory) {
            MovieTitle movieTitle = _libraryService.FindVideo(directory);

            Assert.Equal(movieTitle.Year, "");
        }
    }
}