import {inject} from 'aurelia-framework';
import {Endpoint, Rest} from 'aurelia-api';
import {IMovie} from '../infrastructure/IMovie';

@inject(Endpoint.of('library'))
export class LibraryService {

    constructor(private endpoint: Rest) {
        
    }

    getAllMovies(): Promise<IMovie[]> {
        return this.endpoint.find('movies');
    }

    getMovie(id: string): Promise<IMovie> {
        return this.endpoint.findOne('movies', id);
    }

    deleteMovie(id: string): Promise<void> {
        return this.endpoint.destroyOne('movies', id);
    }
}