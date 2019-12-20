namespace MovieFileLibrary
{
    /// <summary>
    /// MovieDetector helps you to detect movie based on a pre-defined IDetector
    /// </summary>
    public class MovieDetector
    {
        /// <summary>
        /// Movie Detector Method
        /// Method is the way library detects movie info from a file
        /// </summary>
        private readonly IDetectMethod _detector;

        /// <summary>
        /// Use DefaultMethod to detect a movie
        /// </summary>
        public MovieDetector() : base()
        {
            _detector = new DefaultMethod();
        }

        /// <summary>
        /// Use a Custom Method
        /// </summary>
        /// <param name="detector">Custom Method that you want to use</param>
        public MovieDetector(IDetectMethod detector) : base()
        {
            _detector = detector;
        }

        /// <summary>
        /// Get movie info from file using defined Detector
        /// </summary>
        /// <param name="filePath">a string contaning file path</param>
        /// <returns>MovieFile object with basic info regarding the movie</returns>
        public MovieFile GetInfo(string filePath)
        {
            return _detector.GetInfo(filePath);
        }
    }
}
