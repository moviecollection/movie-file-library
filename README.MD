[![Nuget Version][nuget-shield]][nuget]
[![Nuget Version Pre][nuget-shield-pre]][nuget-pre]
[![Nuget Downloads][nuget-shield-downloads]][nuget]
[![License][license-shield]](LICENSE)

# Movie File Library
Movie File Library is a simple C# library that helps you to get basic movie/series info from a file's name.

## Built with
- Demo: .NET Core 3.1
- Tests: xUnit (.NET Core 3.1)

## Target frameworks
- .Net Standard 1.0
- .Net Standard 2.0
- .Net Framework 4.5.1

## Installing
Enter following command in your ```NuGet Package Manager```:
```
Install-Package MovieFileLibrary
```

## How to use

```csharp
// Create an instance of MovieDetector class
var movieDetector = new MovieFileLibrary.MovieDetector();

// Define a test filename
string filePath = "D:\\Monty Python's Flying Circus\\Season 1\\Monty.Pythons.Flying.Circus.S03.E04.avi";

// Call GetInfo to process filename
MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(filePath);

// Print results
Console.WriteLine("Title: {0}", movieFile.Title);
Console.WriteLine("Year: {0}", movieFile.Year);
    
if (movieFile.IsSeries)
{
    Console.WriteLine("IsSeries: {0}", movieFile.IsSeries);
    Console.WriteLine("Season: {0}", movieFile.Season);
    Console.WriteLine("Episode: {0}", movieFile.Episode);
}

Console.WriteLine("FilePath: {0}", movieFile.FilePath);
Console.WriteLine("FileExtension: {0}", movieFile.FileExtension);
Console.WriteLine("Success: {0}", movieFile.Success);   

// Wait for user to press a key to exit
Console.WriteLine("Press Any Key To Exit...");
Console.ReadKey();
```
### Result:
```
Title: Monty Pythons Flying Circus
Year: null
IsSeries: True
Season: 3
Episode: 4
FilePath: D:\Monty Python's Flying Circus\Season 1\Monty.Pythons.Flying.Circus.S03.E04.avi
FileExtension: .avi
Success: True
```

## Change log
Please visit releases page.

## Contributing
If you encountered a problem or you have any suggestions please let me know in the issues.

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieFileLibrary
[nuget-pre]: https://www.nuget.org/packages/MovieFileLibrary/absoluteLatest
[nuget-shield]: https://img.shields.io/nuget/v/MovieFileLibrary.svg?style=flat
[nuget-shield-pre]: https://img.shields.io/nuget/vpre/MovieFileLibrary?label=nuget-preview
[nuget-shield-downloads]: https://img.shields.io/nuget/dt/MovieFileLibrary?color=red
[license-shield]: https://img.shields.io/github/license/peymanr34/movie-file-library.svg?style=flat
