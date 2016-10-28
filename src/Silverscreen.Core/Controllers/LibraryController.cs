using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silverscreen.Model;

namespace Silverscreen.Core.Controllers {
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        [HttpGet("/movies")]
        public IActionResult Get() {

            return new OkObjectResult(1);
        }

        [HttpGet("/movies/{id}")]
        public IActionResult Get(int id)
        {
            //Movie _movie = _movieRepository.GetSingle(m => m.Id == id);
            Movie _movie = new Movie() {
                Id = 1,
                Title = "Contact",
                Plot = "Dr. Ellie Arroway, after years of searching, finds conclusive radio proof of intelligent aliens, who send plans for a mysterious machine.",
                ImdbId = "tt0118884",
                Year = 1997,
                Poster = "https://images-na.ssl-images-amazon.com/images/M/MV5BYWNkYmFiZjUtYmI3Ni00NzIwLTkxZjktN2ZkMjdhMzlkMDc3XkEyXkFqcGdeQXVyNDk3NzU2MTQ@._V1_UX182_CR0,0,182,268_AL_.jpg"
            };

            if(_movie != null) 
            {
                return new OkObjectResult(_movie);
            }
            else 
            {
                return NotFound();
            }
        }

        [HttpPost("/path")]
        public IActionResult Create([FromBody] string libraryPath) {
            return new OkObjectResult(libraryPath);
        }

        //delete path

    }
}