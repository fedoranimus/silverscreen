namespace Silverscreen.Core.Parser {
    public interface IParserService {
        ParsedMovie ParseMovie(string directory);
    }
}