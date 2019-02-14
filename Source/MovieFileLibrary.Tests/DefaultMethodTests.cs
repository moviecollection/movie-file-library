using Xunit;

namespace MovieFileLibrary.Tests
{
    public class DefaultMethodTests
    {
        private DefaultMethod _DefaultMethod;
        private MovieDetector _MovieDetector;

        public DefaultMethodTests()
        {
            _DefaultMethod = new DefaultMethod();
            _MovieDetector = new MovieDetector(_DefaultMethod);
        }

        [Theory]
        // Movie With Year
        [InlineData("The.Spy.Who.Dumped.Me.2018.BluRay.1080p.5.1CH.x264.mkv", "The Spy Who Dumped Me", "2018", false, null, null)]
        // Movie With Year In Name And Year
        [InlineData("Blade.Runner.Black.Out.2022.2017.720p.BluRay.x264.mkv", "Blade Runner Black Out 2022", "2017", false, null, null)]
        // Movie NoSeperator Name
        [InlineData("CaptainAmericaTheWinterSoldier-2014-1080p.mkv", "Captain America The Winter Soldier", "2014", false, null, null)]
        // Series Season And Episode
        [InlineData("True.Detective.S02E07.720p.mkv", "True Detective", null, true, 2, 7)]
        // Series One Char Season
        [InlineData("True.Detective.S2E07.720p.mkv", "True Detective", null, true, 2, 7)]
        // Series One Char Episode
        [InlineData("True.Detective.S02E7.720p.mkv", "True Detective", null, true, 2, 7)]
        // Series One Char Season And Episode
        [InlineData("True.Detective.S2E7.720p.mkv", "True Detective", null, true, 2, 7)]
        // Series Only Season
        [InlineData("True.Detective.S02.720p.mkv", "True Detective", null, true, 2, 1)]
        // Series Only Episode
        [InlineData("True.Detective.E05.720p.mkv", "True Detective", null, true, 1, 5)]
        // Series Seperate Episode And Series
        [InlineData("Modern.Family.S02.E24.720p.mkv", "Modern Family", null, true, 2, 24)]
        // Series Seperate No Dots
        [InlineData("Friends S08 E17 720p.mkv", "Friends", null, true, 8, 17)]
        // Series Seperate No Dots Episode One Char
        [InlineData("The Walking Dead S05E9.avi", "The Walking Dead", null, true, 5, 9)]
        // Series Underline
        [InlineData("13_Reasons_Why_S02E04_x265_720p_WEBRip.mkv", "13 Reasons Why", null, true, 2, 4)]
        // Series Episode Dash E Second Episode
        [InlineData("Seinfeld.S10E17-E18.720p.mkv", "Seinfeld", null, true, 10, 17)]
        // Series Episode E Second Episode
        [InlineData("Seinfeld.S10E17E18.720p.mkv", "Seinfeld", null, true, 10, 17)]
        // Series With Year Before Season And Episodes
        [InlineData("Twisted.2013.S06E19.mp4", "Twisted", "2013", true, 6, 19)]
        public void TestTheDefaultMethod(string filePath, string name, string year, bool isSeries, int? season, int? episode)
        {
            // Get Info
            MovieFile movieFile = _MovieDetector.GetInfo(filePath);

            // Assert
            Assert.True(movieFile.Success);
            Assert.Equal(expected: name, actual: movieFile.Name);
            Assert.Equal(expected: year, actual: movieFile.Year);

            Assert.Equal(expected: isSeries, actual: movieFile.IsSeries);
            Assert.Equal(expected: season, actual: movieFile.Season);
            Assert.Equal(expected: episode, actual: movieFile.Episode);
        }

        [Fact]
        public void TestSuccessFalseAlarmBatch()
        {
            // Source
            string[] filenames =
            {
                "Duplicity.2009.1080p.Farsi.Dubbed.mkv",
                "The.Left.Handed.Gun.1958.WEB-DL.720p.Farsi.Dubbed.mkv",
                "The.Lookout.2007.1080p.Farsi.Dubbed.mkv",
                "The.Magnificent.Seven.1960.1080p.Farsi.Dubbed.mkv",
                "The.Man.in.the.Iron.Mask.1998.1080p.Farsi.Dubbed.mkv",
                "The.Orphanage.2007.1080p.Farsi.Dubbed.mkv",
                "The.Professional.1981.1080p.Fixed.Farsi.Dubbed.mkv",
                "The.Raid.2.Berandal.2014.1080p.5.1CH.Ganool - Dubbed Farsi.mkv",
                "The.Rock.1996.1080p.Farsi.Dubbed.mkv"
            };

            foreach (var item in filenames)
            {
                // Get Info
                MovieFile movieFile = _MovieDetector.GetInfo(item);

                // Assert
                Assert.True(movieFile.Success);
            }
        }
    }
}
