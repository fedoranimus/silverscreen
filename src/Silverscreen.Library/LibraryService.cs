using Silverscreen.Model;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.OMDb;

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
            Console.WriteLine("Adding {0} to {1}", path, directory.DirectoryPath);
            _libraryContext.Directories.Add(directory);
            await _libraryContext.SaveChangesAsync();
            return directory;
        }

        public void ScanLibrary() {
            foreach(var directory in _libraryContext.Directories) {
                Console.WriteLine("Scanning {0}...", directory);
                OmdbClient omdbClient = new OmdbClient();
                DirectoryInfo DirInfo = new DirectoryInfo(directory.DirectoryPath);

                var directories = DirInfo.EnumerateDirectories();
                
                foreach(var d in directories) {
                    Console.WriteLine("Parsing movie: {0}...", d.FullName);
                    AddMovie(omdbClient, d.FullName);
                } 
                Console.WriteLine("Number of subdirectories is {0}", directories.Count());
                Console.WriteLine("{0} scan complete.", directory);
            }
        }

        public List<string> GetDirectories() {
            return _libraryContext.Directories.Select(d => d.DirectoryPath).ToList();
        }

        private async void AddMovie(OmdbClient omdbClient, string directory) {
            //parse title and year from directory.FullName
            var movieData = FindVideo(directory);

            //get metadata from OMDB based on title and year 
            var movie = await FetchMovieMetadata(omdbClient, movieData.Title, movieData.Year);

            //add new Movie to library
            _libraryContext.Movies.Add(movie);
            await _libraryContext.SaveChangesAsync();
        }

        public MovieTitle FindVideo(string directory) {
            //var directoryIdx = directory.LastIndexOf('\\'); //get index where the directory ends
            //var movieTitleString = directory.Substring(directoryIdx + 1); //get all text after this

            var ext = new List<string>{".mkv", ".avi"}; //put this in a configuration area
            var videoFile = new DirectoryInfo(directory).EnumerateFiles("*", SearchOption.AllDirectories) //enumerate all files
                .Where(v => ext.Contains(Path.GetExtension(v.Extension))) //filter by their extensions
                .OrderByDescending(f => f.Length) // order by their size
                .FirstOrDefault(); // get the largest (or null)

            if(videoFile != null) {
                return ParseMovieName(videoFile.Name); //separate title from year
            } else {
                Console.WriteLine("No files in {0}", directory);
                return null;
            }
        }

        public MovieTitle ParseMovieName(string fullFileName) {
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);
            var fileExt = Path.GetExtension(fullFileName);

            string titlePattern = @"^(?<Title>.+?)(\((?<Year>\d+?)\))?$";

            Match match = Regex.Match(fileName, titlePattern);

            var movieTitleDirty = match.Groups["Title"].Value.Trim().Split(',');
            var movieTitle = "";

            if(movieTitleDirty.Length > 1) {
                var moviePrefix = movieTitleDirty.Last().Trim(); // ", The" more than likely
                if(moviePrefix.ToLowerInvariant().Contains("the")) {
                    movieTitle = movieTitleDirty[0]; //would rather do this via regex
                } else {
                    movieTitle = match.Groups["Title"].Value.Trim(); 
                }
            } else {
                movieTitle = match.Groups["Title"].Value.Trim(); 
            }
            
            var movieYear = match.Groups["Year"].Value;            

            return new MovieTitle() {
                Title = movieTitle,
                Year = movieYear,
                Extension = fileExt
            };
        }

        private async Task<Movie> FetchMovieMetadata(OmdbClient omdbClient, string title, string year) {
            Metadata metadata = null;
            if(year == "")
                metadata = await omdbClient.GetMetadata(title);
            else 
                metadata = await omdbClient.GetMetadata(title, year);

            return new Movie() {
                Title = title,
                Year = Convert.ToInt32(year),
                Plot = metadata.Plot,
                ImdbId = metadata.imdbID,
                Poster = metadata.Poster,
                Rating = metadata.imdbRating
            };
        }
    }
}