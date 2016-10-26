using System;
using System.Collections.Generic;

namespace Silverscreen.OMDb 
{    
    public class MetadataList 
    {
        public List<Search> Search = new List<Search>();
    }

    public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
    }
}