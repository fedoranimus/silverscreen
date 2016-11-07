using System;
using Microsoft.AspNetCore.Mvc;
using Silverscreen.Core.Wishlist;

namespace Silverscreen.API.Controllers {
    [Route("api/[controller]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
        {
            _service = service;
        }

        [HttpGet("movies")]
        public IActionResult Get() 
        {
            Console.WriteLine("Get Movies");

            return new OkObjectResult(_service.GetMovies());
        }
    }

}