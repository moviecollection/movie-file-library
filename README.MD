[![Nuget Version](https://img.shields.io/nuget/v/MovieFileLibrary.svg?style=flat)](https://www.nuget.org/packages/MovieFileLibrary)
[![License](https://img.shields.io/github/license/peymanr34/MovieFileLibrary.svg?style=flat)](LICENSE)

# Movie File Library
Movie File Library is a simple C# library that helps you to get basic movie/series info from a file.

## Built With
- Visual Studio Community 2017
- Target Framework: .Net Standard 1.0 
- Testing: xUnit [.Net Core 2.2]
- Demo: .Net Core 2.2

## How to Use

```csharp
// You should specify a Detect Method
// Detect Method is the way this library gets info from a movie file
// If you don't like the default method you can write your own
// If you written one it would be awasome if you share it on GitHub
MovieFileLibrary.DefaultMethod method = new MovieFileLibrary.DefaultMethod();

// Create Movie Detector Library object
// This class helps you to detect movie based on a Method
MovieFileLibrary.MovieDetector movieDetector = new MovieFileLibrary.MovieDetector(method);

// Define a test file path
string filePath = "D:\\Monty Python's Flying Circus\\Season 1\\Monty.Pythons.Flying.Circus.S01.E01.avi";

// Call GetInfo to process file name and return a result
MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(fileInfo);

// Print Results
Console.WriteLine("Name: {0}", movieFile.Name);
Console.WriteLine("Year: {0}", movieFile.Year);
    
if(movieFile.IsSeries)
{
    Console.WriteLine("IsSeries: {0}", movieFile.IsSeries);
    Console.WriteLine("Season: {0}", movieFile.Season);
    Console.WriteLine("Episode: {0}", movieFile.Episode);
}

Console.WriteLine("FilePath: {0}", movieFile.FilePath);
Console.WriteLine("Success: {0}", movieFile.Success);   

// Wait for user to press a key to exit
Console.WriteLine("Press Any Key To Exit...");
Console.ReadKey();
```

## Contributing
If you are interested in writing your own Method you can use ```IDetectMethod``` interface like this example:

```csharp
public class CustomMethod : IDetectMethod
{
    /// <summary>
    /// Gets movie info from file
    /// </summary>
    /// <param name="filePath">a string contaning file path</param>
    /// <returns>MovieFile model with basic info regarding the movie</returns>
    public MovieFile GetInfo(string filePath)
    {
        // Create a MovieFile object and set the filePath via constractor
        MovieFile movieFile = new MovieFile(fileInfo.FullName);

        try
        {
            // TODO: Your fantastic code here ;-)
            // ...

            // Set success to True
            movieFile.Success = true;
        }
        catch (System.Exception)
        {
            // TODO: Handle the exception the way you like it
            // ...

            // Set Success to False because we ran into a exception
            movieFile.Success = false;
        }

        // Return our new MovieFile object
        return movieFile;
    }
}
```

## Feedback
If you encountered a problem please let me know in the issues 

## License
This project is licensed under the [MIT License](https://github.com/peymanr34/moviefilelibrary/blob/master/LICENSE).