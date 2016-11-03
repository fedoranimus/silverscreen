using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Silverscreen.Model;
using Silverscreen.Parser;
using Xunit;
using System;

namespace Silverscreen.Parser.Tests
{
    public class ParsingTests {

        [Fact]    
        public void ParseMovie()
        {
            var _parserService = new ParserService();
            string directory = @"C:\Users\Tim\Documents\GitHub\silverscreen\test\TestData\Videos\10 Cloverfield Lane (2016)";
            ParsedMovie parsedMovie = _parserService.ParseMovie(directory);

            Assert.Equal(parsedMovie.Title, "10 Cloverfield Lane");
            Assert.Equal(parsedMovie.Year, "2016");
            Assert.Equal(parsedMovie.Quality, "720p");
            Assert.Equal(parsedMovie.Extension, ".mkv");
        }
    }
}