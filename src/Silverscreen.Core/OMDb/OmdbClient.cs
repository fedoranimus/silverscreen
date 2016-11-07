using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Silverscreen.Core.OMDb
{
    public class OmdbClient : IOmdbClient
    {
        const string omdbUrl = "https://www.omdbapi.com/"; // Base omdb api URL, https seems to be slow
        public string omdbKey; // A key is required for poster images.
        public Metadata newMovie; // Initialize movie object
        public MetadataList newMovieList; // Initialize movie list object
        private string _apiKey;

        public OmdbClient(string apiKey = "") {
            _apiKey = apiKey;
        }

        private async Task<Metadata> FetchMovieMetadata(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(omdbUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    newMovie = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Metadata>(data));
                    if(Convert.ToBoolean(newMovie.Response))
                        return newMovie;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Metadata> GetMetadata(string title) {
            string query = String.Format("?t={0}", title);
            return await FetchMovieMetadata(query);
        }

        public async Task<Metadata> GetMetadata(string title, string year) {
            string query = String.Format("?t={0}&y={1}", title, year);
            return await FetchMovieMetadata(query);
        }

        public async Task<MetadataList> GetMetadataList(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(omdbUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("?s=" + query);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    newMovieList = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<MetadataList>(data));
                    return newMovieList;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}