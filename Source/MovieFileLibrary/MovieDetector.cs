namespace MovieFileLibrary
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

            // Create a MovieFile object and set the filepath via constructor.
            var movieFile = new MovieFile(filePath);

            // Get filename without extention and replace whitespaces with dots.
            string fileName = Path.GetFileName(filePath);
            string fileNameWx = Path.GetFileNameWithoutExtension(fileName);

            string[] words = GetNormalizedString(fileNameWx, ".").Split('.');

            // Choose the first word as part of the name.
            movieFile.Title = words[0];
            movieFile.Year = null;
            movieFile.Season = null;
            movieFile.Episode = null;
            movieFile.IsSeries = false;

            int i;
            for (i = 1; i < words.Length; i++)
            {
                string item = words[i].Trim();

                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }

                // Check if current item is a Year value or a Season value or a Episode value.
                if (IsYear(item))
                {
                    // Check for movies with year in name [e.g The Legend of 1900 (1998)].
                    // Skip first item to avoid issues with movies like "2001: A Space Odyssey (1968)".
                    // Also Skip current item and get the last one.
                    string lastYear = words.Skip(1).Where(x => IsYear(x) && x != item).LastOrDefault();

                    if (lastYear is null)
                    {
                        // We don't have another year in filename, so we use the current one.
                        movieFile.Year = item;
                    }
                    else
                    {
                        // We have another year value, therefore treat the current item as part of movie title.
                        movieFile.Year = lastYear;
                        movieFile.Title += " " + item;
                    }

                    // For cases that we have year in series file name!
                    if (!IsSeasonPresent(words) && !IsEpisodePresent(words))
                    {
                        break;
                    }
                }
                else if (IsSeason(item))
                {
                    // In this case we have a "S" in words that indicates season number for a tv series.
                    // Remove Season indicator from current item then split the item using "e" to get episode numeric value.
                    // This is for cases where Season and Episode values are squashed together (e.g. MovieName.S01E02).
                    string[] sp = item.Substring(1, item.Length - 1).ToUpperInvariant().Split('E');

                    if (!int.TryParse(sp[0], out int seasonvalue))
                    {
                        movieFile.Season = null;
                        movieFile.Episode = null;
                        movieFile.IsSeries = false;
                        break;
                    }

                    movieFile.IsSeries = true;
                    movieFile.Season = seasonvalue;

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
                    // In this case we have a episode indicator.
                    // This case happens when season number and episode number are separated (e.g MovieName.S05.E08).
                    movieFile.IsSeries = true;

                    // Remove episode indicator character.
                    string e = item.Substring(1, item.Length - 1).ToUpperInvariant();

                    // Get the numeric value.
                    if (int.TryParse(e, out int episodevalue))
                    {
                        movieFile.Episode = episodevalue;
                    }

                    break;
                }
                else if (IsSeasonAndEpisodeWithX(item))
                {
                    // For cases where Season and Episode values are seperated by a "x" character.
                    string[] split = item.ToUpperInvariant().Split('X');

                    if (split.Length == 2 && int.TryParse(split[0], out int seasonValue) && int.TryParse(split[1], out int episodeValue))
                    {
                        movieFile.IsSeries = true;
                        movieFile.Season = seasonValue;
                        movieFile.Episode = episodeValue;

                        break;
                    }

                    // Treat current item as part of the movie title.
                    movieFile.Title += " " + item;
                    continue;
                }
                else
                {
                    // It's [probably] part of movie name.
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

            // Return MovieFile object
            movieFile.IsSuccess = true;
            return movieFile;
        }

        private static string GetNormalizedString(string str, string seperator)
        {
            var items = new[] { " ", "(", ")", "_", "-", "..", "–", "[", "]", "{", "}" };

            foreach (string item in items)
            {
                if (str.Contains(item))
                {
                    str = str.Replace(item, seperator);
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

        /// <summary>
        /// Check if we have Season indicator in a string (e.g: S02 or S3).
        /// </summary>
        /// <param name="item">The string to test.</param>
        /// <returns>true if the value indicates an season value.</returns>
        private static bool IsSeason(string item)
        {
            // e.g. S01E01
            if (Regex.IsMatch(item, @"^S([0-9]{1,2})E([0-9]{1,2})", RegexOptions.IgnoreCase))
            {
                return true;
            }

            // e.g. S01
            if (Regex.IsMatch(item, @"^S([0-9]{1,2})$", RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if we have Episode indicator in a string (Like: E02 or E3).
        /// </summary>
        /// <param name="item">The string to test.</param>
        /// <returns>true if the value indicates an episode value.</returns>
        private static bool IsEpisode(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return false;
            }

            return char.ToUpperInvariant(item[0]) == 'E' && item.Length > 1 && char.IsNumber(item[1]);
        }

        /// <summary>
        /// Check if Season indicator presents on a string array.
        /// </summary>
        /// <param name="words">An array to test.</param>
        /// <returns>true if array contains an season indicator value.</returns>
        private static bool IsSeasonPresent(string[] words)
        {
            return words.Any(x => IsSeason(x));
        }

        /// <summary>
        /// Check if Episode indicator presents on a string array.
        /// </summary>
        /// <param name="words">An array to test.</param>
        /// <returns>true if array contains an episode indicator value.</returns>
        private static bool IsEpisodePresent(string[] words)
        {
            return words.Any(x => IsEpisode(x));
        }
    }
}
