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
        private readonly IDetectMethod _Detector;

        /// <summary>
        /// Use Default Detector to detect movie
        /// </summary>
        public MovieDetector() : base()
        {
            _Detector = new DefaultMethod();
        }

        /// <summary>
        /// Use Custom Method
        /// </summary>
        /// <param name="detector">Custom Method that you want to use</param>
        public MovieDetector(IDetectMethod detector) : base()
        {
            _Detector = detector;
        }

        /// <summary>
        /// Get movie info from file using defined Detector
        /// </summary>
        /// <param name="filePath">a string contaning file path</param>
        /// <returns>MovieFile model with basic info regarding the movie</returns>
        public MovieFile GetInfo(string filePath)
        {
            return _Detector.GetInfo(filePath);
        }
    }
}
