namespace Silverscreen.Model {
    public class Directory : IEntityBase
    {
        private Directory() 
        {
            
        }
        public Directory(string path) 
        {   
            DirectoryPath = path;
        }

        public int Id { get; set; }
        public string DirectoryPath { get; set; }
    }
}