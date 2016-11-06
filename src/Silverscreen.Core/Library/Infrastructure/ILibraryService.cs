using System.Collections.Generic;
using Silverscreen.Core.Model;
using System.Threading.Tasks;
public interface ILibraryService {
    List<Movie> GetMovies();
    Task ScanLibrary();
    Task<LibraryDirectory> AddDirectory(string path);
    Movie GetMovie(int id);

    List<string> GetDirectories();

}