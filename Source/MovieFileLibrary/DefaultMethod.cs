namespace MovieFileLibrary
{
    /// <summary>
    /// This is a Default Method to retrive movie info
    /// Method is the way we get info from a movie file
    /// If you don't like the default method you can write your own
    /// If you written one it would be awasome if you share it on GitHub
    /// </summary>
    public class DefaultMethod : IDetectMethod
    {
        /// <summary>
        /// Get movie info from file
        /// </summary>
        /// <param name="filePath">a string contaning file path</param>
        /// <returns>MovieFile model with basic info regarding the movie</returns>
        public MovieFile GetInfo(string filePath)
        {
            // Create a MovieFile object and set the filepath via constractor
            MovieFile movieFile = new MovieFile(filePath);

            try
            {
                // Get Filename Without Extention and Replace Whitespaces With Dots
                string FileName = System.IO.Path.GetFileName(filePath);
                string value = System.IO.Path.GetFileNameWithoutExtension(FileName).Replace(" ", ".");

                // Remove Extra Characters
                string[] removeStrs = new[] { "(", ")", "_", "-", ".." };
                foreach (string item in removeStrs)
                {
                    if (value.Contains(item))
                    {
                        value = value.Replace(item, ".");
                    }
                }

                // Split The Atom!
                string[] words = value.Split('.');

                // Choose First Word as Name
                movieFile.Name = words[0];

                // Set Defaults
                movieFile.Year = null;
                movieFile.Season = null;
                movieFile.Episode = null;
                movieFile.IsSeries = false;

                // Enumerate in words
                for (int i = 1; i < words.Length; i++)
                {
                    // Trim current word
                    string item = words[i].Trim();

                    // If it was empty go to next word
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }

                    // Check if current word is a Year value or a Season value or a Episode value
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^(19|20)[0-9][0-9]"))
                    {
                        movieFile.Year = item;

                        // For cases that we have year in series file name!
                        if (!IsSeasonPresentsOnWords(words) && !IsEpisodePresentsOnWords(words))
                            break;
                    }
                    else if (IsMeetsSeasonCondition(item))
                    {
                        // In this case We have a "S" in words that indicates season number for a tv series
                        movieFile.IsSeries = true;

                        // Remove Season indicator from word and also split word using "e"
                        // season and episode values are something like this: MovieName.S01E02
                        string[] sp = item.Substring(1, item.Length - 1).ToLower().Split('e');

                        // Try get Season value using 
                        bool seasonResult = int.TryParse(sp[0], out int seasonvalue);
                        movieFile.Season = seasonvalue;

                        // If we have a Episode value (because of previews split)
                        if (sp.Length >= 2)
                        {
                            // Same TryParse method
                            bool episodeResult = int.TryParse(sp[1], out int episodevalue);
                            movieFile.Episode = episodevalue;

                            break;
                        }
                        else if (!IsEpisodePresentsOnWords(words))
                        {
                            movieFile.Episode = 1;
                            break;
                        }
                    }
                    else if (IsMeetsEpisodeCondition(item))
                    {
                        // In this case We have a "E" in words that indicates season number.
                        // This case happens when season number and episode number are sperated with space
                        // or when season value is not present.
                        movieFile.IsSeries = true;

                        // If we didn't have a Season value
                        // happens when season value is not present like: MovieName.E03)
                        if (movieFile.Season == null)
                            movieFile.Season = 1;

                        // Split like before
                        var e = item.Substring(1, item.Length - 1).ToLower();

                        // And TryParse
                        bool episodeResult = int.TryParse(e, out int episodevalue);
                        movieFile.Episode = episodevalue;

                        break;
                    }
                    else
                    {
                        // if current word is non of cases so [probably] is part of movie name
                        movieFile.Name += " " + item;
                    }
                }

                // Set success to True
                movieFile.Success = true;
            }
            catch (System.Exception)
            {
                // Set Success to False because we ran into a exception
                movieFile.Success = false;
            }

            // Return our new MovieFile object
            return movieFile;
        }


        /// <summary>
        /// Check if we have Season indicator in a string (Like: S02 or S3)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsMeetsSeasonCondition(string item)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                return false;

            return (char.ToLower(item[0]) == 's' && item.Length > 2 && char.IsNumber(item[1]));
        }

        /// <summary>
        /// Check if we have Episode indicator in a string (Like: E02 or E3)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsMeetsEpisodeCondition(string item)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                return false;

            return (char.ToLower(item[0]) == 'e' && item.Length > 1 && char.IsNumber(item[1]));
        }

        /// <summary>
        /// Check if Season indicator presents on a string array
        /// </summary>
        /// <param name="words">string array containing splited file name</param>
        /// <returns></returns>
        private bool IsSeasonPresentsOnWords(string[] words)
        {
            foreach (var item in words)
            {
                if (IsMeetsSeasonCondition(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if Episode endicator presents on a string array
        /// </summary>
        /// <param name="words">string array containing splited file name</param>
        /// <returns></returns>
        private bool IsEpisodePresentsOnWords(string[] words)
        {
            foreach (var item in words)
            {
                if (IsMeetsEpisodeCondition(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
