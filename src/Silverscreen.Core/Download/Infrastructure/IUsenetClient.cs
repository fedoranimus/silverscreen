using System.Threading.Tasks;
using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public interface IUsenetClient
    {
        DownloadProtocol Protocol { get; }
        Task<string> Download(RemoteMovie remoteMovie);
    }
}