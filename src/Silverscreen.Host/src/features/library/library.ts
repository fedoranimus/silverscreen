import {autoinject} from 'aurelia-framework';
import {IMovie} from '../../infrastructure/IMovie';
import {LibraryService} from '../../services/libraryService';

@autoinject
export class Library {
    movies: IMovie[] = [];

    constructor(private libraryService: LibraryService) {

    }

    async attached() {
        try {
            this.movies = await this.libraryService.getAllMovies();
        } catch(e) {
            console.error(e);
        }
    }
}