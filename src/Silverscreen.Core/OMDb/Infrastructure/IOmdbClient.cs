using System.Threading.Tasks;

namespace Silverscreen.Core.OMDb {
    public interface IOmdbClient {
        Task<Metadata> GetMetadata(string title);
        Task<Metadata> GetMetadata(string title, string year);
        Task<MetadataList> GetMetadataList(string query);
    }
}