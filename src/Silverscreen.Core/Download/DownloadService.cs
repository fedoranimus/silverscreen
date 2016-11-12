using System;
using Microsoft.Extensions.Logging;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public class DownloadService 
    {
        private readonly IDownloadClientProvider _downloadClientProvider;
        public DownloadService(IDownloadClientProvider downloadClientProvider)
        {
            _downloadClientProvider = downloadClientProvider;
        }

        public void Download(RemoteMovie remoteMovie)
        {
            try 
            {
                var downloadClient = _downloadClientProvider.GetDownloadClient(remoteMovie.Release.Protocol);
                //downloadClient.Download(remoteMovie);
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}