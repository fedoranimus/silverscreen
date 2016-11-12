using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public interface IDownloadClientProvider 
    {
        DownloadClient GetDownloadClient(DownloadProtocol protocol);
    }

}