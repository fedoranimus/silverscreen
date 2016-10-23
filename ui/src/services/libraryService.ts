import {autoinject} from 'aurelia-framework';
import {Config} from 'aurelia-api';
import {IMovie} from '../infrastructure/IMovie';

export class LibraryService {
    private movieEndpoint = null; //of type Rest|null?

    constructor(private config: Config) {
        this.movieEndpoint = config.getEndpoint('api');
    }

    getAllMovies(): Promise<IMovie[]> {
        return this.movieEndpoint.find('movies');
    }

    getMovie(id: string): Promise<IMovie> {
        return this.movieEndpoint.findOne('movie', id);
    }

    deleteMovie(id: string): Promise<void> {
        return this.movieEndpoint.destroyOne('movie', id);
    }
}