using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // You should specify a Detect Method
            // Detect Method is the way this library gets info from a movie file
            // If you don't like the default method you can write your own
            // If you written one it would be awasome if you share it on GitHub
            MovieFileLibrary.DefaultMethod method = new MovieFileLibrary.DefaultMethod();

            // Create Movie Detector Library object
            // This class helps you to detect movie based on a Method
            MovieFileLibrary.MovieDetector movieDetector = new MovieFileLibrary.MovieDetector(method);

            // Bunch of samples
            string[] files =
            {
                "D:\\Doctor Who 2005 S09E05 720p.mkv",
                "D:\\True.Detective.S03E06.720p.x264.mkv",
                "D:\\Monty.Pythons.Flying.Circus.S03.E04.avi",
                "D:\\The.Grand.Tour.S02E05.1080p.WEB-DL.6CH.x265.HEVC.mkv",
                "D:\\Bohemian.Rhapsody.2018.720p.BRRip.x265.mkv"
            };

            // Enumerate simple files and call MovieDetector and print out results
            foreach (var file in files)
            {
                // Call GetInfo to process file name and return a result
                MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(file);

                // Print Results
                Console.WriteLine("Name: {0}", movieFile.Name);
                Console.WriteLine("Year: {0}", movieFile.Year);

                if (movieFile.IsSeries)
                {
                    Console.WriteLine("IsSeries: {0}", movieFile.IsSeries);
                    Console.WriteLine("Season: {0}", movieFile.Season);
                    Console.WriteLine("Episode: {0}", movieFile.Episode);
                }

                Console.WriteLine("FilePath: {0}", movieFile.FilePath);
                Console.WriteLine("Success: {0}", movieFile.Success);

                Console.WriteLine("--------------------------------------------");
            }

            // Wait For User Exit
            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey();
        }
    }
}
