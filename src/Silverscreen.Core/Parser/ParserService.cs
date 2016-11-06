using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Silverscreen.Core.Parser {
    public class ParserService : IParserService {
        public ParserService() {
            Console.WriteLine("Instantiated ParserService");
        }

        public ParsedMovie ParseMovie(string directory) {
            var ext = new List<string>{".mkv", ".avi"}; //put this in a configuration area
            var videoFile = new DirectoryInfo(directory).EnumerateFiles("*", SearchOption.AllDirectories) //enumerate all files
                .Where(v => ext.Contains(Path.GetExtension(v.Extension))) //filter by their extensions
                .OrderByDescending(f => f.Length) // order by their size
                .FirstOrDefault(); // get the largest (or null)

            if(videoFile != null) {
                Console.WriteLine("Parsing {0}", videoFile.Name);
                var parsedMovie = ParseMovieFile(videoFile.Name); //separate title from year
                if(parsedMovie.Title != "") 
                {
                    parsedMovie.FileSize = videoFile.Length;
                    parsedMovie.Extension = videoFile.Extension;

                    return parsedMovie;
                }
                else 
                {
                    return null;
                }
            } else {
                Console.WriteLine("No video file found in {0}", directory);
                return null;
            }
        }

        private ParsedMovie ParseMovieDirectory(string fullFileName) {
            var directory = Path.GetDirectoryName(fullFileName);
            var fileExt = Path.GetExtension(fullFileName);

            return parseSimpleTitle(directory);
        }

        private ParsedMovie ParseMovieFile(string fullFileName) {
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);
            var fileExt = Path.GetExtension(fullFileName);

            return parseSimpleTitle(fileName);
        }

        private ParsedMovie parseSimpleTitleFormat(string unparsedTitle) {
            
            
            return new ParsedMovie() {

            };
        }

        private ParsedMovie parseSimpleTitle(string unparsedTitle) {
            string titlePattern = @"^(?<Title>.+?)\((?<Year>[12]\d\d\d)\)(?<Quality>\ \d{1,4}[ipk])?$"; //{Title} {(Year)} {Quality}

            Match match = Regex.Match(unparsedTitle, titlePattern);

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
            var movieQuality = match.Groups["Quality"].Value.Trim();

            return new ParsedMovie() {
                            Title = movieTitle,
                            Year = movieYear,
                            Quality = movieQuality
                        };
        }

        private ParsedMovie parseDownloadedTitle(string unparsedTitle) {
            string pattern = @"

                ^(?<Title>.+?)                 # Movie Name up to year and resolution
                (?!\.[12]\d\d\d\.\d{,3}[ip]\.) # Year and resolution foward negative look ahead as an a pattern anchor
                \.                             # Non captured due to only explicitly capturing.
                (?<Year>\d\d\d\d)              # Capture Year, etc...
                \.
                (?<Resolution>[^.]+)
                \.
                (?<Format>[^.]+) 

                ";


                Match match = Regex.Match(unparsedTitle, pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace); // Allows us to comment pattern only.

                var title = Regex.Replace(match.Groups["Title"].Value, @"\.", " ");
                var year = match.Groups["Year"].Value;

                return new ParsedMovie() {
                            Title = title,
                            Year = year
                        };
        }
    }
}