namespace MovieFileLibrary
{
    /// <summary>
    /// Movie file represents a movie file with basic movie info
    /// </summary>
    public class MovieFile : object
    {
        /// <summary>
        /// Creates a Movie File object
        /// </summary>
        /// <param name="filepath">Movie File Path</param>
        public MovieFile(string filepath)
            : base()
        {
            FilePath = filepath;
            FileExtension = System.IO.Path.GetExtension(filepath);
        }

        public string Title { get; set; }
        public string Year { get; set; }
        public bool IsSeries { get; set; }
        public int? Season { get; set; }
        public int? Episode { get; set; }
        public bool Success { get; set; }
        public string FilePath { get; private set; }
        public string FileExtension { get; private set; }
    }
}
