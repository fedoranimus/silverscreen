using System.Collections.Generic;
using System.Threading.Tasks;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Indexers
{
    public interface IIndexerService
    {
        void AddIndexer(Indexer indexer);
        void DeleteIndexer(Indexer indexer);
        List<Indexer> GetIndexers();
        Task<List<DownloadCandidate>> GetCandidates(string imdbId);
    }
}