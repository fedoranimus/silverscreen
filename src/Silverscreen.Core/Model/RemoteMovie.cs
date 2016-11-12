namespace Silverscreen.Core.Model
{
    public class RemoteMovie
    {
        public ReleaseInfo Release { get; set; }
        public bool DownloadAllowed { get; set; }

        public override string ToString()
        {
            return Release.Title;
        }
    }
}