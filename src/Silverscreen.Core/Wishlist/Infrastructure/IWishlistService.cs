using System.Collections.Generic;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Wishlist {
    public interface IWishlistService {
        List<Movie> GetMovies();
    }
}