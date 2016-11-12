using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Indexers {
    public class IndexerService : IIndexerService
    {
        private readonly HttpClient _httpClient;
        public IndexerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private List<Indexer> _indexers { get; set; }
        public async Task<List<DownloadCandidate>> GetCandidates(string imdbId) 
        {
            List<DownloadCandidate> candidates = new List<DownloadCandidate>();

            foreach(var indexer in _indexers) {
                var query = string.Format("https://{0}/api?apikey={1}&t=movie&imdbid={2}&o=JSON", indexer.ApiUrl, indexer.ApiKey, imdbId);
                var response = await _httpClient.GetAsync(query);
                if(response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //var movieCandidates = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<ReleaseCandidate>>(data));
                    //candidates.AddRange(movieCandidates);
                }
            }

            return candidates;
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