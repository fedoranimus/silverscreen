import {MetadataProviderService} from './metadataProviderService';
import {IMovieMetaData} from '../infrastructure/IMovie';

export class OMDBProviderService extends MetadataProviderService{

    constructor() {
        super("OMDB", "http://www.omdbapi.com");
    }

    public findMovie(name: string, year?: string): Promise<IMovieMetaData> {
        return null;
    }

}