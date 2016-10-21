import {MetadataProviderService} from './metadataProviderService';
import {IMovieMetaData} from '../infrastructure/IMovie';

export class OMDBProviderService extends MetadataProviderService{
    constructor() {
        super();
    }
    public findMovie(name: string, year?: string): Promise<IMovieMetaData> {
        return null;
    }

}