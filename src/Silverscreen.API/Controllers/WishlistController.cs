using System;
using Microsoft.AspNetCore.Mvc;
using Silverscreen.Core.Wishlist;
using Silverscreen.Core.OMDb;

namespace Silverscreen.API.Controllers {
    [Route("api/[controller]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _service;
        private readonly IOmdbClient _omdbClient;

        public WishlistController(IWishlistService service, IOmdbClient omdbClient)
        {
            _service = service;
            _omdbClient = omdbClient;
        }

        [HttpGet("movies")]
        public IActionResult Get() 
        {
            Console.WriteLine("Get Movies");

            return new OkObjectResult(_service.GetMovies());
        }
    }

}