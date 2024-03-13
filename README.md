# MovieFileLibrary

A dotnet library for extracting basic information from a movie file name.

[![NuGet Version][nuget-shield]][nuget]
[![NuGet Downloads][nuget-shield-dl]][nuget]

## Installing

You can install this package via the `Package Manager Console`:

```powershell
Install-Package MovieFileLibrary
```

## How to use

Create a new instance of the `MovieDetector` class:

```csharp
// using MovieFileLibrary;
var detector = new MovieDetector();
```

### Movies

You can get the `Title` and `Year` of a movie via the `GetInfo` method:

```csharp
var movieFile = detector.GetInfo("D:\\Oppenheimer.2023.1080p.mkv");

// Title: Oppenheimer
// Year: 2023
```

### Series

You can also get the `Season` and `Episode` of a tv show:

```csharp
var movieFile = detector.GetInfo("D:\\Frasier.S06E22.1080p.mp4");

// Title: Frasier
// Year: null
// IsSeries: True
// Season: 6
// Episode: 22
```

### IMDb

You can also get the `ImdbId` of a movie:

```csharp
var movieFile = detector.GetInfo("D:\\Amelie.2001.1080p.{imdb-tt0211915}.mkv");

// Title: Amelie
// Year: 2001
// ImdbId: tt0211915
```

Other styles are also supported:

```csharp
var movieFile = detector.GetInfo("D:\\No.Time.to.Die.2021.1080p.[imdbid-tt2382320].mkv");

// Title: No Time to Die
// Year: 2021
// ImdbId: tt2382320
```

### More Examples

Please see the demo project for more examples.

## Changelog

Please visit the [releases][releases] for more information.

## License

This project is licensed under the [MIT License](LICENSE).

[nuget]: https://www.nuget.org/packages/MovieFileLibrary/absoluteLatest
[nuget-shield]: https://img.shields.io/nuget/vpre/MovieFileLibrary?label=Preview
[nuget-shield-dl]: https://img.shields.io/nuget/dt/MovieFileLibrary?label=Downloads&color=red
[releases]: https://github.com/moviecollection/movie-file-library/releases
