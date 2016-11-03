using System.Collections.Generic;
using Silverscreen.Model;
using System.Threading.Tasks;
public interface ILibraryService {
    List<Movie> GetMovies();
    Task ScanLibrary();
    Task<Directory> AddDirectory(string path);
    Movie GetMovie(int id);

    List<string> GetDirectories();

}