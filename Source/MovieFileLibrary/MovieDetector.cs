﻿namespace MovieFileLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The default implementation of <see cref="IMovieDetector"/> interface.
    /// </summary>
    public class MovieDetector : IMovieDetector
    {
        /// <inheritdoc/>
        public MovieFile GetInfo(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace", nameof(filePath));
            }

            var movieFile = new MovieFile(filePath);

            string fileName = Path.GetFileName(filePath);
            string fileNameWx = Path.GetFileNameWithoutExtension(fileName);

            string[] words = GetNormalizedString(fileNameWx, ".").Split('.');

            // Usually the first item is part of the title.
            movieFile.Title = words[0];

            int i;
            for (i = 1; i < words.Length; i++)
            {
                string item = words[i].Trim();

                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }

                if (IsYear(item))
                {
                    // The Legend of 1900 (1998)
                    // 2001: A Space Odyssey (1968)
                    string lastYear = words.Skip(1)
                        .Where(x => IsYear(x) && x != item)
                        .LastOrDefault();

                    if (lastYear is null)
                    {
                        movieFile.Year = item;
                    }
                    else
                    {
                        movieFile.Year = lastYear;
                        movieFile.Title += " " + item;
                    }

                    // Scenes.from.a.Marriage.1973.E01.mkv
                    if (!IsSeasonPresent(words) && !IsEpisodePresent(words))
                    {
                        break;
                    }
                }
                else if (IsSeason(item))
                {
                    var separator = item.IndexOf("Ep", StringComparison.OrdinalIgnoreCase) >= 0 ? "EP" : "E";

                    // Normal.People.S01E04.1080p.mkv
                    string[] sp = item.Substring(1, item.Length - 1).ToUpperInvariant()
                        .Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                    if (!int.TryParse(sp[0], out int season))
                    {
                        movieFile.Season = null;
                        movieFile.Episode = null;
                        movieFile.IsSeries = false;
                        break;
                    }

                    movieFile.IsSeries = true;
                    movieFile.Season = season;

                    var episodes = new List<int>();

                    foreach (var episode in sp.Skip(1))
                    {
                        if (int.TryParse(episode, out int value))
                        {
                            episodes.Add(value);
                        }
                    }

                    if (episodes.Count != 0)
                    {
                        movieFile.Episode = episodes.First();
                        break;
                    }

                    if (!IsEpisodePresent(words))
                    {
                        break;
                    }
                }
                else if (IsEpisode(item))
                {
                    var separator = item.IndexOf("Ep", StringComparison.OrdinalIgnoreCase) >= 0 ? "EP" : "E";

                    // The.Grand.Tour.S04.E04.1080p.mkv
                    movieFile.IsSeries = true;

                    string e = item.Substring(separator.Length, item.Length - separator.Length).ToUpperInvariant();

                    if (int.TryParse(e, out int episode))
                    {
                        movieFile.Episode = episode;
                    }

                    break;
                }
                else if (IsSeasonAndEpisodeWithX(item))
                {
                    // Top Gear 17x03 HDTV.mp4
                    string[] split = item.ToUpperInvariant().Split('X');

                    if (split.Length == 2 &&
                        int.TryParse(split[0], out int seasonValue) &&
                        int.TryParse(split[1], out int episodeValue))
                    {
                        movieFile.IsSeries = true;
                        movieFile.Season = seasonValue;
                        movieFile.Episode = episodeValue;

                        break;
                    }

                    movieFile.Title += " " + item;
                    continue;
                }
                else
                {
                    movieFile.Title += " " + item;
                }
            }

            var remaining = words.Skip(i + 1).ToArray();

            if (movieFile.IsSeries && remaining.Any(x => x.Equals("Special", StringComparison.OrdinalIgnoreCase)))
            {
                movieFile.IsSpecialEpisode = true;
            }

            // Find the imdb id (e.g. Batman Begins (2005) {imdb-tt0372784}.mkv).
            var imdb1 = Array.FindIndex(remaining, t => t.Equals("imdb", StringComparison.OrdinalIgnoreCase));
            var imdb2 = Array.FindIndex(remaining, t => t.Equals("imdbid", StringComparison.OrdinalIgnoreCase));

            if (imdb1 >= 0)
            {
                movieFile.ImdbId = remaining.ElementAtOrDefault(imdb1 + 1);
            }
            else if (imdb2 >= 0)
            {
                movieFile.ImdbId = remaining.ElementAtOrDefault(imdb2 + 1);
            }

            movieFile.IsSuccess = true;
            return movieFile;
        }

        private static string GetNormalizedString(string str, string separator)
        {
            var items = new[] { " ", "(", ")", "_", "-", "..", "–", "[", "]", "{", "}" };

            foreach (string item in items)
            {
                if (str.Contains(item))
                {
                    str = str.Replace(item, separator);
                }
            }

            return str;
        }

        private static bool IsSeasonAndEpisodeWithX(string item)
        {
            return Regex.IsMatch(item, "([0-9]{1,2})([xX])([0-9]{1,2})");
        }

        private static bool IsYear(string item)
        {
            return Regex.IsMatch(item, "^(19|20)[0-9][0-9]");
        }

        private static bool IsSeason(string item)
        {
            // S01E01
            if (Regex.IsMatch(item, @"^S([0-9]{1,2})Ep?([0-9]{1,2})", RegexOptions.IgnoreCase))
            {
                return true;
            }

            // S01
            if (Regex.IsMatch(item, @"^S([0-9]{1,2})$", RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }

        private static bool IsEpisode(string item)
        {
            return Regex.IsMatch(item, @"^Ep?([0-9]{1,2})", RegexOptions.IgnoreCase);
        }

        private static bool IsSeasonPresent(string[] words)
        {
            return words.Any(x => IsSeason(x));
        }

        private static bool IsEpisodePresent(string[] words)
        {
            return words.Any(x => IsEpisode(x));
        }
    }
}
