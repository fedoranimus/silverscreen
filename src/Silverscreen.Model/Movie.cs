using System.ComponentModel;

namespace Silverscreen.Model {

    public enum QualityType {
        
        //[Description("480p")]
        SD = 0,
        //[Description("720p")]
        HD = 1,
        //[Description("1080p")]
        FullHD = 2,
        //[Description("4k")]
        QuadHD = 3,
    }
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

        public string Rating { get; set; }

        public QualityType Quality { get; set; }

    }
}