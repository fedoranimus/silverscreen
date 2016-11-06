using System;
using Microsoft.AspNetCore.Mvc;
using Silverscreen.Core.Model;
using Silverscreen.Core.Library;

namespace Silverscreen.API.Controllers {
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private readonly ILibraryService _service;

        public LibraryController(ILibraryService service)
        {
            _service = service;
        }

        [HttpGet("movies")]
        public IActionResult Get() 
        {
            Console.WriteLine("Get Movies");

            return new OkObjectResult(_service.GetMovies());
        }

        [HttpGet("movies/scan")]
        public void Scan()
        {
            _service.ScanLibrary();
        }

        [HttpGet("movies/{id}")]
        public IActionResult GetMovie(int id)
        {
            Movie _movie = _service.GetMovie(id);

            if(_movie != null) 
            {
                return new OkObjectResult(_movie);
            }
            else 
            {
                return NotFound();
            }
        }

        [HttpPostAttribute("movies/directories")]
        public IActionResult AddDirectory([FromBody] DirectoryContract directory) {
            Console.WriteLine("Hit AddDirectory");
            string path = directory.directoryPath;
            if(path == null) {
                return BadRequest();
            }
            
            Console.WriteLine(path);
            var result = _service.AddDirectory(path);
            return new OkObjectResult(path);
            //return new CreatedAtRouteResult("GetDirectory", new { id = result.Id }, path);
        }

        [HttpGetAttribute("movies/directories")]
        public IActionResult GetDirectories() {
            return new OkObjectResult(_service.GetDirectories());
        }

        // [HttpGetAttribute("/movies/directories/{id}")]
        // public IActionResult GetDirectory(int id) {
        //     return OkObjectResult(_service.GetDirectory(id));
        // }

        //delete path

    }
}