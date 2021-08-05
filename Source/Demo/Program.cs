using System;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            // Create a new instance of the MovieDetector class.
            var movieDetector = new MovieFileLibrary.MovieDetector();

            // Sample files.
            string[] files =
            {
                "D:\\Doctor Who 2005 S09E05 720p.mkv",
                "D:\\True.Detective.S03E06.720p.x264.mkv",
                "D:\\Monty.Pythons.Flying.Circus.S03.E04.avi",
                "D:\\The.Grand.Tour.S02E05.1080p.WEB-DL.6CH.x265.HEVC.mkv",
                "D:\\Bohemian.Rhapsody.2018.720p.BRRip.x265.mkv",
                "D:\\Top Gear 17x03 HDTV.mp4"
            };

            foreach (var file in files)
            {
                // Get information from file name.
                MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(file);

                Console.WriteLine("Title: {0}", movieFile.Title);
                Console.WriteLine("Year: {0}", movieFile.Year);

                if (movieFile.IsSeries)
                {
                    Console.WriteLine("IsSeries: {0}", movieFile.IsSeries);
                    Console.WriteLine("Season: {0}", movieFile.Season);
                    Console.WriteLine("Episode: {0}", movieFile.Episode);
                }

                Console.WriteLine("Path: {0}", movieFile.Path);
                Console.WriteLine("Extension: {0}", movieFile.Extension);
                Console.WriteLine("IsSuccess: {0}", movieFile.IsSuccess);

                Console.WriteLine("--------------------------------------------");
            }

            // Wait for user to press a key to exit.
            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey();
        }
    }
}
