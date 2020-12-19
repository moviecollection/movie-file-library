using Xunit;

namespace MovieFileLibrary.Tests
{
    public class DetectionTests
    {
        private readonly MovieDetector _detector;

        public DetectionTests()
        {
            _detector = new MovieDetector();
        }

        [Theory]
        [InlineData("Iron.Man.2.2010.720p.Encode.S1N4.mkv", "Iron Man 2", "2010")]
        [InlineData("The Legend of 1900 (1998).mkv", "The Legend of 1900", "1998")]
        [InlineData("Happythankyoumoreplease.2010.mkv", "Happythankyoumoreplease", "2010")]
        [InlineData("The.Spy.Who.Dumped.Me.2018.BluRay.1080p.5.1CH.x264.mkv", "The Spy Who Dumped Me", "2018")]
        [InlineData("2001.A.Space.Odyssey.1968.1080p.BluRay.6CH.x265.HEVC.mkv", "2001 A Space Odyssey", "1968")]
        [InlineData("Blade.Runner.Black.Out.2022.2017.720p.BluRay.x264.mkv", "Blade Runner Black Out 2022", "2017")]
        public void ReturnDataForMoviesShouldBeCorrect(string filePath, string title, string year)
        {
            MovieFile movieFile = _detector.GetInfo(filePath);

            Assert.True(movieFile.IsSuccess);
            Assert.False(movieFile.IsSeries);

            Assert.Equal(title, actual: movieFile.Title);
            Assert.Equal(year, actual: movieFile.Year);

            Assert.Null(movieFile.Season);
            Assert.Null(movieFile.Episode);
        }

        [Theory]
        [InlineData("Friends S08 E17 720p.mkv", "Friends", 8, 17)]
        [InlineData("Top Gear 17x03 HDTV.mp4", "Top Gear", 17, 3)]
        [InlineData("Seinfeld.S10E17E18.720p.mkv", "Seinfeld", 10, 17)]
        [InlineData("Seinfeld.S10E17-E18.720p.mkv", "Seinfeld", 10, 17)]
        [InlineData("Twisted.2013.S06E19.mp4", "Twisted", 6, 19, "2013")]
        [InlineData("True.Detective.S02.720p.mkv", "True Detective", 2, 1)]
        [InlineData("True.Detective.E05.720p.mkv", "True Detective", 1, 5)]
        [InlineData("The Walking Dead S05E9.avi", "The Walking Dead", 5, 9)]
        [InlineData("True.Detective.S2E7.720p.mkv", "True Detective", 2, 7)]
        [InlineData("True.Detective.S02E7.720p.mkv", "True Detective", 2, 7)]
        [InlineData("True.Detective.S2E07.720p.mkv", "True Detective", 2, 7)]
        [InlineData("True.Detective.S02E07.720p.mkv", "True Detective", 2, 7)]
        [InlineData("Modern.Family.S02.E24.720p.mkv", "Modern Family", 2, 24)]
        [InlineData("Sherlock S03 E02 [The Sign of Three] 720p.mp4", "Sherlock", 3, 2)]
        [InlineData("13_Reasons_Why_S02E04_x265_720p_WEBRip.mkv", "13 Reasons Why", 2, 4)]
        [InlineData("The.Count.of.Monte.Cristo.1998.E04.720p.BluRay.2CH.x265.HEVC.mkv", "The Count of Monte Cristo", 1, 4, "1998")]
        public void ReturnDataForSeriesShouldBeCorrect(string filePath, string title, int? season, int? episode, string year = null)
        {
            MovieFile movieFile = _detector.GetInfo(filePath);

            Assert.True(movieFile.IsSuccess);
            Assert.True(movieFile.IsSeries);

            Assert.Equal(title, actual: movieFile.Title);
            Assert.Equal(year, actual: movieFile.Year);
            Assert.Equal(season, actual: movieFile.Season);
            Assert.Equal(episode, actual: movieFile.Episode);
        }

        [Theory]
        [InlineData("The.Rock.1996.1080p.Dubbed.mkv")]
        [InlineData("Duplicity.2009.1080p.Dubbed.mkv")]
        [InlineData("The.Lookout.2007.1080p.Dubbed.mkv")]
        [InlineData("The.Orphanage.2007.1080p.Dubbed.mkv")]
        [InlineData("The.Magnificent.Seven.1960.1080p.Dubbed.mkv")]
        [InlineData("The.Professional.1981.1080p.Fixed.Dubbed.mkv")]
        [InlineData("The.Man.in.the.Iron.Mask.1998.1080p.Dubbed.mkv")]
        [InlineData("The.Left.Handed.Gun.1958.WEB-DL.720p.Dubbed.mkv")]
        [InlineData("The.Raid.2.Berandal.2014.1080p.5.1CH.Ganool - Dubbed.mkv")]
        public void ShouldNotReturnFalseNegative(string filePath)
        {
            MovieFile movieFile = _detector.GetInfo(filePath);
            Assert.True(movieFile.IsSuccess);
        }

        [Fact]
        public void ShouldThrowExceptionOnNullFilePath()
        {
            Assert.Throws<System.ArgumentException>(() => _detector.GetInfo(null));
        }
    }
}
