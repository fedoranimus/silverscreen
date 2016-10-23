namespace Silverscreen.Model {
    public class Movie : IEntityBase
    {
        public Movie() {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public string ImdbId { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
    }
}