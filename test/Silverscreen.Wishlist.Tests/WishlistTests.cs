using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Silverscreen.Core.Model;
using Silverscreen.Core.Wishlist;
using Silverscreen.Core.OMDb;
using Xunit;
using System;

namespace Silverscreen.Wishlist.Tests
{
    public class WishlistTests
    {

        private static DbContextOptions<MediaCollectionContext> CreateNewContextOptions() {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<MediaCollectionContext>();
            builder.UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider);

           return builder.Options;
        }

        [Fact]
        public async void AddMovieToWishlist() 
        {
            var options = CreateNewContextOptions();

            using(var context = new MediaCollectionContext(options))
            {
                var omdbClient = new OmdbClient();
                var _service = new WishlistService(context, omdbClient);
                string imdbId = "tt1211837"; //Doctor Strange
                QualityType desiredQuality = QualityType.HD;

                Movie movie = await _service.AddMovie(imdbId, desiredQuality);

                Assert.Equal(movie.Title, "Doctor Strange");
                Assert.Equal(movie.Year, 2016);
            }
        }
    }
}