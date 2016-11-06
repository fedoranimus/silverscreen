namespace Silverscreen.Core.Model {
    public class LibraryDirectory : IEntityBase
    {
        private LibraryDirectory() 
        {
            
        }
        public LibraryDirectory(string path) 
        {   
            DirectoryPath = path;
        }

        public int Id { get; set; }
        public string DirectoryPath { get; set; }
    }
}