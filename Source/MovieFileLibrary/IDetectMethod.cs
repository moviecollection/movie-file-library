namespace MovieFileLibrary
{
    /// <summary>
    /// Defines an approach to get basic movie info from a movie file
    /// </summary>
    public interface IDetectMethod
    {
        MovieFile GetInfo(string filePath);
    }
}
