using Silverscreen.Core.Model;

namespace Silverscreen.Core.Download
{
    public class DownloadClientDefinition : IDownloadClientDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public bool UseSSL { get; set; }
        public DownloadProtocol Protocol { get; set; }
    }
}