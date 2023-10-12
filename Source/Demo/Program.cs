using System;

// Create a new instance of the MovieDetector class.
var movieDetector = new MovieFileLibrary.MovieDetector();

// Sample files.
string[] files =
{
    "D:\\Top Gear 17x03 HDTV.mp4",
    "D:\\Doctor Who 2005 S09E05 720p.mkv",
    "D:\\True.Detective.S03E06.720p.x264.mkv",
    "D:\\Monty.Pythons.Flying.Circus.S03.E04.avi",
    "D:\\Bohemian.Rhapsody.2018.720p.BRRip.x265.mkv",
    "D:\\The.Grand.Tour.S02E05.1080p.WEB-DL.6CH.x265.HEVC.mkv",
    "D:\\Amelie.2001.1080p.BluRay.6CH.x265.HEVC.{imdb-tt0211915}.mkv",
    "D:\\No.Time.to.Die.2021.1080p.10bit.BluRay.x265.[imdbid-tt2382320].mkv",
};

foreach (var file in files)
{
    // Get information from file name.
    MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(file);

    Console.WriteLine($"Title: {movieFile.Title}");
    Console.WriteLine($"Year: {movieFile.Year}");

    if (movieFile.IsSeries)
    {
        Console.WriteLine($"IsSeries: {movieFile.IsSeries}");
        Console.WriteLine($"Season: {movieFile.Season}");
        Console.WriteLine($"Episode: {movieFile.Episode}");
    }
    else
    {
        Console.WriteLine($"ImdbId: {movieFile.ImdbId}");
    }

    Console.WriteLine($"Path: {movieFile.Path}");
    Console.WriteLine($"Extension: {movieFile.Extension}");
    Console.WriteLine($"IsSuccess: {movieFile.IsSuccess}");

    Console.WriteLine("--------------------------------------------");
}

// Wait for user to press a key to exit.
Console.WriteLine("Press Any Key To Exit...");
Console.ReadKey();
