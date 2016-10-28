using System.Collections.Generic;
using Silverscreen.Model;
public interface ILibraryService {
    List<Movie> getMovies();
    void ScanLibrary();
    void addDirectory(string path);
    Movie getItem(int id);

}