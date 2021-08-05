# MovieFileLibrary
MovieFileLibrary helps you get basic information from a movie file's name. 

[![Nuget Version][nuget-shield]][nuget]
[![Nuget Downloads][nuget-shield-dl]][nuget]

## Installing
You can install this package by entering the following command into your `Package Manager Console`:
```powershell
Install-Package MovieFileLibrary
```

## How to use
This example shows you how to use `MovieFileLibrary` in your project:
```csharp
// Create a new instance of the MovieDetector class.
var movieDetector = new MovieFileLibrary.MovieDetector();

// Define a test file path.
string filePath = "D:\\Monty Python's Flying Circus\\Season 3\\Monty.Pythons.Flying.Circus.S03.E04.avi";

// Call GetInfo to process the filename.
MovieFileLibrary.MovieFile movieFile = movieDetector.GetInfo(filePath);

// Print the results.
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
```
### Result:
```
Title: Monty Pythons Flying Circus
Year: null
IsSeries: True
Season: 3
Episode: 4
Path: D:\Monty Python's Flying Circus\Season 3\Monty.Pythons.Flying.Circus.S03.E04.avi
Extension: .avi
IsSuccess: True
```

## License
This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieFileLibrary/absoluteLatest
[nuget-shield]: https://img.shields.io/nuget/vpre/MovieFileLibrary?label=Preview
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieFileLibrary?label=Downloads&color=red