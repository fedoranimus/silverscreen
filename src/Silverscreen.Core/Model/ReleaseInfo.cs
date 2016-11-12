namespace Silverscreen.Core.Model
{
    public class ReleaseInfo : IEntityBase {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public DownloadProtocol Protocol { get; set; }
        public string DownloadUrl { get; set; }
        
    }
}