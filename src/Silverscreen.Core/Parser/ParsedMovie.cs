namespace Silverscreen.Core.Parser {
    public class ParsedMovie {
        public string Title { get; set; }
        public string Year { get; set; }

        public string Quality { get; set; }

        public string Extension { get; set; }
        public long FileSize { get; set; }
    }
}