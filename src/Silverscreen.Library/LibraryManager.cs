using Silverscreen.Model;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.OMDb;

namespace Silverscreen.Library {
    public class LibraryManager {
        Library library;
        public LibraryManager() {
            library = new Library();
        }

        public Library getLibrary(){
            return library; //maybe just return metadata?
        }

        public Movie getItem(int id) {
            return library.Movies.Find(m => m.Id == id);
        }

        public void addPath(string path) {
            library.Paths.Add(path);
            //rerun
        }

        public void ScanLibrary() {
            OmdbClient omdbClient = new OmdbClient();
            DirectoryInfo DirInfo = new DirectoryInfo(@"\\Plex\Movies\");

            var directories = DirInfo.EnumerateDirectories();
            
            foreach(var d in directories) {
                Console.WriteLine("{0}", d.FullName);
                addMovie(omdbClient, d.FullName);
            } 
            Console.WriteLine("Number of Directories is {0}", directories.Count());
        }

        private async void addMovie(OmdbClient omdbClient, string directory) {
            //parse title and year from directory.FullName
            var movieData = FindVideo(directory);

            //get metadata from OMDB based on title and year 
            var movie = await FetchMovieMetadata(omdbClient, movieData.Title, movieData.Year);

            //add new Movie to library
            library.Movies.Add(movie);
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

        public MovieTitle ParseMovieName(string fileName) {
            string titlePattern = @"^(?<Title>.+)\((?<Year>\d+)\)$";

            Match match = Regex.Match(fileName, titlePattern);

            var movieTitleDirty = match.Groups["Title"].Value.Trim().Split(',');
            var movieTitle = movieTitleDirty[0]; //would rather do this via regex

            if(movieTitleDirty.Length > 1) {
                var moviePrefix = movieTitleDirty[1].Trim(); // ", The" more than likely
            }
            
            var movieYear = match.Groups["Year"].Value;            

            return new MovieTitle() {
                Title = movieTitle,
                Year = movieYear
            };
        }

        private async Task<Movie> FetchMovieMetadata(OmdbClient omdbClient, string title, string year) {
            var metadata = await omdbClient.GetMetadata(title, year);

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