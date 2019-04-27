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
        public MovieFile(string filepath) : base()
        {
            FilePath = filepath;
            FileExtension = System.IO.Path.GetExtension(filepath);
        }

        /// <summary>
        /// Movie Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Movie Year
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// MovieType
        /// </summary>
        public bool IsSeries { get; set; }

        /// <summary>
        /// Season
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// Episode
        /// </summary>
        public int? Episode { get; set; }

        /// <summary>
        /// Result
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// File Path
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Extension of File
        /// </summary>
        public string FileExtension { get; private set; }
    }
}
