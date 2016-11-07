using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.Core.Model;
using Silverscreen.Core.OMDb;
using Silverscreen.Core.Parser;

namespace Silverscreen.Core.Library {
    public class LibraryService : ILibraryService {
        private readonly MediaCollectionContext _mediaCollectionContext;
        private readonly IParserService _parserService;
        public LibraryService(MediaCollectionContext mediaCollectionContext, IParserService parserService) {
            _mediaCollectionContext = mediaCollectionContext;
            _parserService = parserService;
        }

        public MediaCollectionContext GetLibrary(){
            return _mediaCollectionContext; //maybe just return metadata?
        }

        public List<Movie> GetMovies() {
            return _mediaCollectionContext.Movies.Where(m => m.inLibrary == true).ToList(); //only get the movies in the library
        }

        public Movie GetMovie(int id)
        {
            return _mediaCollectionContext.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public async Task<LibraryDirectory> AddDirectory(string path) {
            var directory = new LibraryDirectory(path);
            Console.WriteLine("Adding {0} as {1}", path, directory.DirectoryPath);
            _mediaCollectionContext.Directories.Add(directory);
            await _mediaCollectionContext.SaveChangesAsync();
            return directory;
        }

        public async Task ScanLibrary() {
            Console.WriteLine("Starting Library Scan...");
            var directories = _mediaCollectionContext.Directories.ToList();
            foreach(var directory in directories) {
                Console.WriteLine("Scanning {0}...", directory.DirectoryPath);
                OmdbClient omdbClient = new OmdbClient();
                DirectoryInfo DirInfo = new DirectoryInfo(directory.DirectoryPath);

                var movieDirectories = DirInfo.EnumerateDirectories();
                
                foreach(var d in movieDirectories) {
                    Console.WriteLine("Found movie directory: {0}...", d.FullName);
                    await AddMovie(omdbClient, d.FullName);
                    Console.WriteLine("--------------");
                } 
                await _mediaCollectionContext.SaveChangesAsync();
                Console.WriteLine("Number of subdirectories is {0}", directories.Count());
                Console.WriteLine("{0} scan complete.", directory.DirectoryPath);
            }
        }

        public List<string> GetDirectories() {
            return _mediaCollectionContext.Directories.Select(d => d.DirectoryPath).ToList();
        }

        private async Task AddMovie(OmdbClient omdbClient, string directory) {
            var movie = await CorrelateVideoToMetadata(omdbClient, directory);
            if(movie != null) {
                movie.inLibrary = true;
                _mediaCollectionContext.Movies.Add(movie); //add new Movie to library/update if it exists
                Console.WriteLine("Added: {0} as {1} ({2})", movie.ImdbId, movie.Title, movie.Year.ToString());
            }
            else
            {
                Console.WriteLine("{0} doesn't contain a valid movie", directory);
            }
        }

        private async Task<Movie> CorrelateVideoToMetadata(OmdbClient omdbClient, string directory) {
            //parse title and year from directory.FullName
            var movieData = _parserService.ParseMovie(directory);

            //get metadata from OMDB based on title and year 
            if(movieData != null) 
            {
                Console.WriteLine("Fetching metadata for {0} ({1})", movieData.Title, movieData.Year);
                var movie = await FetchMovieMetadata(omdbClient, movieData.Title, movieData.Year);
                if(movie != null) 
                {
                    return movie;
                }
                else 
                {
                    Console.WriteLine("No Metadata for {0} found, adding to unknown list...", movieData.Title);
                    return null;
                }
            }
            else 
            {
                return null;
            }
        }

        private async Task<Movie> FetchMovieMetadata(OmdbClient omdbClient, string title, string year) {
            Metadata metadata = null;

            if(year == "")
                metadata = await omdbClient.GetMetadata(title);
            else 
                metadata = await omdbClient.GetMetadata(title, year);

            if(metadata != null) 
            {
                return new Movie() {
                    Title = title,
                    Year = Convert.ToInt32(metadata.Year),
                    Plot = metadata.Plot,
                    ImdbId = metadata.imdbID,
                    Poster = metadata.Poster,
                    Rating = metadata.imdbRating
                };
            } 
            else
            {
                return null;
            } 
        }
    }
}