using Silverscreen.Model;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.OMDb;
using Silverscreen.Parser;

namespace Silverscreen.Library {
    public class LibraryService : ILibraryService {
        private readonly LibraryContext _libraryContext;
        public LibraryService(LibraryContext libraryContext) {
            _libraryContext = libraryContext;
        }

        public LibraryContext GetLibrary(){
            return _libraryContext; //maybe just return metadata?
        }

        public List<Movie> GetMovies() {
            return _libraryContext.Movies.ToList();
        }

        public Movie GetItem(int id)
        {
            return _libraryContext.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public async Task<Silverscreen.Model.Directory> AddDirectory(string path) {
            var directory = new Silverscreen.Model.Directory(path);
            Console.WriteLine("Adding {0} as {1}", path, directory.DirectoryPath);
            _libraryContext.Directories.Add(directory);
            await _libraryContext.SaveChangesAsync();
            return directory;
        }

        public void ScanLibrary() {
            Console.WriteLine("Starting Library Scan...");
            foreach(var directory in _libraryContext.Directories) {
                Console.WriteLine("Scanning {0}...", directory.DirectoryPath);
                OmdbClient omdbClient = new OmdbClient();
                DirectoryInfo DirInfo = new DirectoryInfo(directory.DirectoryPath);

                var directories = DirInfo.EnumerateDirectories();
                
                foreach(var d in directories) {
                    Console.WriteLine("Parsing movie: {0}...", d.FullName);
                    AddMovie(omdbClient, d.FullName);
                } 
                Console.WriteLine("Number of subdirectories is {0}", directories.Count());
                Console.WriteLine("{0} scan complete.", directory.DirectoryPath);
            }
        }

        public List<string> GetDirectories() {
            return _libraryContext.Directories.Select(d => d.DirectoryPath).ToList();
        }

        private async void AddMovie(OmdbClient omdbClient, string directory) {
            var movie = await CorrelateVideoToMetadata(omdbClient, directory);

            if(movie != null) {
                //add new Movie to library
                _libraryContext.Movies.Add(movie);
                Console.WriteLine("Added: {0} as {1} ({2})", movie.ImdbId, movie.Title, movie.Year.ToString());
                await _libraryContext.SaveChangesAsync();
            }
        }

        private async Task<Movie> CorrelateVideoToMetadata(OmdbClient omdbClient, string directory) {
            //parse title and year from directory.FullName
            var movieData = FindMovie(directory);

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
                    Console.WriteLine("No Metadata for {0} found, checking directory...", movieData.Title);

                    //check directory name for metadata
                    //if metadata by directory cannot be found
                    //add to list of unknown items 
                }
            }
        }

        private async Task<Movie> FetchMovieMetadata(OmdbClient omdbClient, string title, string year) {
            Metadata metadata = null;
            if(year == "")
                metadata = await omdbClient.GetMetadata(title);
            else 
                metadata = await omdbClient.GetMetadata(title, year);

            return new Movie() {
                Title = title,
                Year = Convert.ToInt32(metadata.Year),
                Plot = metadata.Plot,
                ImdbId = metadata.imdbID,
                Poster = metadata.Poster,
                Rating = metadata.imdbRating
            };
        }
    }
}