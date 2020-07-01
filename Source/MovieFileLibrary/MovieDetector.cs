namespace MovieFileLibrary
{
    /// <summary>
    /// The <c>MovieDetector</c> class helps you get movie info.
    /// </summary>
    public class MovieDetector
    {
        private readonly IDetectMethod _detector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieDetector"/> class with the default implementation.
        /// </summary>
        public MovieDetector()
            : base()
        {
            _detector = new DefaultMethod();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieDetector"/> class.
        /// </summary>
        /// <param name="detector">An implementation of the <see cref="IDetectMethod"/> interface.</param>
        public MovieDetector(IDetectMethod detector)
            : base()
        {
            _detector = detector;
        }

        /// <summary>
        /// Gets movie info from a filename.
        /// </summary>
        /// <param name="filePath">The path of a movie file.</param>
        /// <returns>New instance of the <see cref="MovieFile"/> class with basic info regarding the movie.</returns>
        public MovieFile GetInfo(string filePath)
        {
            return _detector.GetInfo(filePath);
        }
    }
}
