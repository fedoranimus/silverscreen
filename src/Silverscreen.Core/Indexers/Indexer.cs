using System.Collections.Generic;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Indexers {
    public class Indexer : IIndexer {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool RssEnabled { get; set; }
        public bool SearchEnabled { get; set; }
        public DownloadProtocol Protocol { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public List<int> Categories { get; set; }
        public Indexer() {

        }
    }
}