using System;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public class DownloadClientProvider : IDownloadClientProvider
    {
        public DownloadClientProvider() 
        {

        }

        public DownloadClient GetDownloadClient(DownloadProtocol protocol)
        {
            throw new NotImplementedException();
        }
    }
}