import {inject} from 'aurelia-framework';
import {Endpoint, Rest, Config} from 'aurelia-api';
import {IMovie} from '../infrastructure/IMovie';

@inject(Config)
export class LibraryService {
    private endpoint;
    
    constructor(private config: Config) {
        console.log("Library Service Created");
        this.endpoint = config.getEndpoint('library');
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