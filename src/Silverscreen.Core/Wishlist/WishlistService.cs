using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.Core.Model;
using Silverscreen.Core.OMDb;

namespace Silverscreen.Core.Wishlist {
    public class WishlistService : IWishlistService {
        private readonly MediaCollectionContext _mediaCollectionContext;

        private readonly IOmdbClient _omdbClient;
        public WishlistService(MediaCollectionContext mediaCollectionContext, IOmdbClient omdbClient) {
            _mediaCollectionContext = mediaCollectionContext;
            _omdbClient = omdbClient;
        }

        public List<Movie> GetMovies() {
            return _mediaCollectionContext.Movies.Where(m => m.inLibrary == false).ToList(); //movies not in the library are on the wishlist
        }

        public async Task<Movie> AddMovie(string imdbId, QualityType desiredQuality) {
            Movie movie = _mediaCollectionContext.Movies.FirstOrDefault(m => m.ImdbId == imdbId);
            if(movie != null) 
            {
                movie.DesiredQuality = desiredQuality;
            }
            else 
            {
                try 
                {
                    var metadata = await _omdbClient.GetMetadataByImdbId(imdbId);
                    
                    movie = new Movie() {
                        Title = metadata.Title,
                        Plot = metadata.Plot,
                        ImdbId = imdbId,
                        Year = Convert.ToInt32(metadata.Year),
                        Poster = metadata.Poster,
                        Rating = metadata.imdbRating,
                        inLibrary = false,
                        DesiredQuality = desiredQuality
                    };

                    await _mediaCollectionContext.AddAsync(movie);
                }
                catch(Exception e)
                {
                    throw e;
                }

            }

            await _mediaCollectionContext.SaveChangesAsync();

            return movie;
        }
    }
}