using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download.Clients.NzbGet
{
    public class NzbGetClient : INzbGetClient
    {

        private readonly IUsenetClient _usenetClient;
        public NzbGetClient(IUsenetClient usenetClient) 
        {
            _usenetClient = usenetClient;
        }

        public DownloadProtocol Protocol
        {
            get
            {
                 return _usenetClient.Protocol;
            }
        }
    }
}