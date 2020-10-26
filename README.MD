[![Nuget Version][nuget-shield]][nuget]
[![Nuget Version Pre][nuget-shield-pre]][nuget-pre]
[![Nuget Downloads][nuget-shield-downloads]][nuget]

# Movie File Library
Movie File Library helps you get basic information from a movie file's name. 

## Built with
- Demo: .NET Core 3.1
- Tests: xUnit (.NET Core 3.1)

## Target frameworks
- .NET Standard 1.0
- .NET Standard 2.0
- .NET Framework 4.5.1

## Installing
You can install this package by entering the following command into your `Package Manager Console`:
```powershell
Install-Package MovieFileLibrary
```

## How to use

```csharp
// Create an instance of MovieDetector class
var movieDetector = new MovieFileLibrary.MovieDetector();

// Define a test filename
string filePath = "D:\\Monty Python's Flying Circus\\Season 3\\Monty.Pythons.Flying.Circus.S03.E04.avi";

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
FilePath: D:\Monty Python's Flying Circus\Season 3\Monty.Pythons.Flying.Circus.S03.E04.avi
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