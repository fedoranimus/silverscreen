using System.Collections.Generic;
using System.Threading.Tasks;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Wishlist {
    public interface IWishlistService {
        List<Movie> GetMovies();
        Task<Movie> AddMovie(string imdbId, QualityType desiredQuality);
    }
}