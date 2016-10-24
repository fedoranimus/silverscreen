using Silverscreen.Model;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Silverscreen.Library {
    public class LibraryManager {

        private const string OmbdUrl = "http://www.omdbapi.com/";
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
            DirectoryInfo DirInfo = new DirectoryInfo(@"\\Plex\Movies\");

            var directories = DirInfo.EnumerateDirectories();
            
            foreach(var d in directories) {
                Console.WriteLine("{0}", d.FullName);
                addMovie(d.FullName);

            } 
            Console.WriteLine("Number of Directories is {0}", directories.Count());
        }

        private async void addMovie(string directory) {
            //parse title and year from directory.FullName
            var movieData = ParseMovieDirectoryName(directory);

            //get metadata from OMDB based on title and year 
            var movie = await FetchMovieMetadata(movieData.Title, movieData.Year);

            //add new Movie to library
            library.Movies.Add(movie);
        }

        public MovieTitle ParseMovieDirectoryName(string directory) {
            var directoryIdx = directory.LastIndexOf('\\'); //get index where the directory ends
            var movieTitleString = directory.Substring(directoryIdx + 1); //get all text after this

            //separate title from year
            string titlePattern = @"^(?<Title>.+)\((?<Year>\d+)\)$";

            Match match = Regex.Match(movieTitleString, titlePattern);

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

        private async Task<Movie> FetchMovieMetadata(string title, string year) {
            string query = String.Format("?t={0}?y={1}", title, year);


            using(var client = new HttpClient()) {
                using(var response = await client.GetAsync(new Uri(OmbdUrl))) {
                    string result = await response.Content.ReadAsStringAsync();
                    var metadata = JsonConvert.DeserializeObject<OmdbResponse>(result);

                    return new Movie() {
                                    Title = title,
                                    Year = year,
                                    Plot = metadata.Plot,
                                    ImdbId = metadata.imdbID,
                                    Poster = metadata.Poster,
                                    Rating = metadata.imdbRating
                                };
                }
            }  
        }
    }
}