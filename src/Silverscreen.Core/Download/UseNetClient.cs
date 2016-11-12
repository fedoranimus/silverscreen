using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public class UsenetClient : IUsenetClient
        {
            private readonly HttpClient _httpClient;
            private readonly IDownloadClient _downloadClient;
            protected UsenetClient(IDownloadClient downloadClient, HttpClient httpClient)
            {
                _downloadClient = downloadClient;
                _httpClient = httpClient;
            }

            public DownloadProtocol Protocol
            {
                get
                {
                    return DownloadProtocol.Usenet;
                }
            }

            public async Task<string> Download(RemoteMovie remoteMovie)
            {
                var url = remoteMovie.Release.DownloadUrl;
                var filename = remoteMovie.Release.Title + ".nzb";

                byte[] nzbData;

                try
                {
                    nzbData = await _httpClient.GetByteArrayAsync(url);
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }

                throw new NotImplementedException();
            }
        }
}