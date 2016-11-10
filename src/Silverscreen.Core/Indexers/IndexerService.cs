using System.Collections.Generic;
using System.Threading.Tasks;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Indexers {
    public class IndexerService : IIndexerService
    {
        private List<Indexer> _indexers { get; set; }
        public async Task<List<DownloadCandidate>> GetCandidates(string imdbId) 
        {
            return new List<DownloadCandidate>();
        }

        public List<Indexer> GetIndexers() 
        {
            return _indexers;
        }

        public void AddIndexer(Indexer indexer) 
        {
            _indexers.Add(indexer);
        }

        public void DeleteIndexer(Indexer indexer) 
        {
            _indexers.Remove(indexer);
        }
    }
}

//https://api.dognzb.cr/api?apikey=1e1592a7ff097403fddb9c51978b4e5b&t=movie&imdbid=0088763
//Usenet API Docs: http://newznab.readthedocs.io/en/latest/misc/api/#movie-search