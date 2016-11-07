using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Wishlist {
    public class WishlistService : IWishlistService {
        private readonly MediaCollectionContext _mediaCollectionContext;
        public WishlistService(MediaCollectionContext mediaCollectionContext) {
            _mediaCollectionContext = mediaCollectionContext;
        }

        public List<Movie> GetMovies() {
            return _mediaCollectionContext.Movies.Where(m => m.inLibrary == false).ToList(); //movies not in the library are on the wishlist
        }

        public async Task<Movie> AddMovie(string imdbId) {
            return new Movie() {

            };
        }
    }
}