namespace MovieFileLibrary
{
    /// <summary>
    /// The <see cref="IDetectMethod"/> interface.
    /// </summary>
    public interface IDetectMethod
    {
        /// <summary>
        /// Gets movie info from a filename.
        /// </summary>
        /// <param name="filePath">The path of a movie file.</param>
        /// <returns>New instance of the <see cref="MovieFile"/> class with basic info regarding the movie.</returns>
        MovieFile GetInfo(string filePath);
    }
}
