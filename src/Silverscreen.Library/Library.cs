using System.Collections.Generic;
using Silverscreen.Model;

namespace Silverscreen.Library {
    public class Library : IEntityBase {
        public Library () {
            List<string> Paths = new List<string>();
            List<Movie> Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public List<string> Paths { get; set; }
        public List<Movie> Movies { get; set;}
    }
}