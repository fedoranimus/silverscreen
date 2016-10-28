using System.Collections.Generic;
using Silverscreen.Model;

namespace Silverscreen.Library {
    public class Library : IEntityBase {
        public Library () {
            List<string> Directories = new List<string>();
            List<Movie> Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public List<string> Directories { get; set; }
        public List<Movie> Movies { get; set;}
    }
}