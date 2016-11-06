import {IMovie} from '../../infrastructure/IMovie';
import {LibraryService} from '../../services/libraryService';

export class Library {
    movies: IMovie[] = [];

    constructor(private libraryService: LibraryService) {

    }

    attached() {
        this.libraryService.getAllMovies()
            .then( movies => {
                this.movies = movies;
            })
            .catch(e => {
                console.error(e);
            });
    }
}