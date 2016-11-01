using Silverscreen.Model;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Silverscreen.OMDb;

namespace Silverscreen.Renamer {
    public class RenamerService {
        public RenamerService() {

        }

        public void BatchRename() {

        }

        public void RenameMovie() {

        }

        private string RenameFromMetaData(FileInfo fileInfo, Metadata metadata) {
            return "";
        }

        private string RenameFromStandardDownloadedMovie(FileInfo fileInfo) {
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


                Match match = Regex.Match(fileInfo.Name, pattern, RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace); // Allows us to comment pattern only.

                var title = Regex.Replace(match.Groups["Title"].Value, @"\.", " ");
                var year = match.Groups["Year"].Value;
                var format = match.Groups["Format"].Value;

                var filename = string.Join(" ", title, year) + "." + format;
                return filename;
        }
    }
}