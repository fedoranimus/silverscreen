namespace Silverscreen.Model {
    public class Directory : IEntityBase
    {
        public Directory(string path) 
        {   
            DirectoryPath = path;
        }

        public int Id { get; set; }
        public string DirectoryPath { get; set; }
    }
}