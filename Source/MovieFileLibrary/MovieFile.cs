namespace MovieFileLibrary
{
    using System;

    /// <summary>
    /// A <c>MovieFile</c> represents information about a movie file.
    /// </summary>
    public class MovieFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieFile"/> class.
        /// </summary>
        /// <param name="filePath">The path of a movie file.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="filePath"/> is null or whitespace.
        /// </exception>
        public MovieFile(string filePath)
            : base()
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace", nameof(filePath));
            }

            Path = filePath;
            Extension = System.IO.Path.GetExtension(filePath);
        }

        /// <summary>
        /// Gets or sets movie title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets movie year.
        /// </summary>
        public string? Year { get; set; }

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
        /// Gets or sets imdb id if exists.
        /// </summary>
        public string? ImdbId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it's a special episode.
        /// </summary>
        public bool IsSpecialEpisode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether detection was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets path of the movie file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets extension of the movie file.
        /// </summary>
        public string? Extension { get; private set; }
    }
}
