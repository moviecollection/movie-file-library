namespace MovieFileLibrary
{
    /// <summary>
    /// A <c>MovieFile</c> represents information about a movie file.
    /// </summary>
    public class MovieFile : object
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieFile"/> class.
        /// </summary>
        /// <param name="filepath">The path of a movie file.</param>
        public MovieFile(string filepath)
            : base()
        {
            FilePath = filepath;
            FileExtension = System.IO.Path.GetExtension(filepath);
        }

        /// <summary>
        /// Gets or sets movie title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets movie year.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether movie file is a series.
        /// </summary>
        public bool IsSeries { get; set; }

        /// <summary>
        /// Gets or sets series season number if exists.
        /// </summary>
        public int? Season { get; set; }

        /// <summary>
        /// Gets or sets series episode number if exists.
        /// </summary>
        public int? Episode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether detection was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets path of the movie file.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets extension of the movie file.
        /// </summary>
        public string FileExtension { get; private set; }
    }
}
