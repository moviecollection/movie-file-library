using System;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace MovieFileLibrary.Tests
{
    [UsesVerify]
    public class DetectionTests
    {
        private readonly MovieDetector _detector;

        public DetectionTests()
        {
            _detector = new MovieDetector();
        }

        [Theory]
        [InlineData("Iron.Man.2.2010.720p.Encode.S1N4.mkv")]
        [InlineData("The Legend of 1900 (1998).mkv")]
        [InlineData("Happythankyoumoreplease.2010.mkv")]
        [InlineData("The.Spy.Who.Dumped.Me.2018.BluRay.1080p.5.1CH.x264.mkv")]
        [InlineData("2001.A.Space.Odyssey.1968.1080p.BluRay.6CH.x265.HEVC.mkv")]
        [InlineData("Blade.Runner.Black.Out.2022.2017.720p.BluRay.x264.mkv")]
        public Task ReturnDataForMoviesShouldBeCorrect(string filePath)
        {
            MovieFile movieFile = _detector.GetInfo(filePath);

            return Verifier.Verify(movieFile)
                .UseParameters(filePath);
        }

        [Theory]
        [InlineData("Friends S08 E17 720p.mkv")]
        [InlineData("Top Gear 17x03 HDTV.mp4")]
        [InlineData("Seinfeld.S10E17E18.720p.mkv")]
        [InlineData("Seinfeld.S10E17-E18.720p.mkv")]
        [InlineData("Twisted.2013.S06E19.mp4")]
        [InlineData("True.Detective.S02.720p.mkv")]
        [InlineData("True.Detective.E05.720p.mkv")]
        [InlineData("The Walking Dead S05E9.avi")]
        [InlineData("True.Detective.S2E7.720p.mkv")]
        [InlineData("True.Detective.S02E7.720p.mkv")]
        [InlineData("True.Detective.S2E07.720p.mkv")]
        [InlineData("True.Detective.S02E07.720p.mkv")]
        [InlineData("Modern.Family.S02.E24.720p.mkv")]
        [InlineData("Sherlock S03 E02 [The Sign of Three] 720p.mp4")]
        [InlineData("13_Reasons_Why_S02E04_x265_720p_WEBRip.mkv")]
        [InlineData("The.Count.of.Monte.Cristo.1998.E04.720p.BluRay.2CH.x265.HEVC.mkv")]
        [InlineData("Solar.Opposites.S00E04.A.Very.Solar.Holiday.Opposites.Special.1080p.mkv")]
        [InlineData("Doctor.Who.Specials.E03.Voyage.Of.The.Damned.2007.10bit.x265.1080p.BluRay.mkv")]
        [InlineData("Mr.Bean.S01.(Special).Mr.Beans.Wedding.480p.WEB-DL.x264.mkv")]
        public Task ReturnDataForSeriesShouldBeCorrect(string filePath)
        {
            MovieFile movieFile = _detector.GetInfo(filePath);

            return Verifier.Verify(movieFile)
                .UseParameters(filePath);
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
            Assert.Throws<ArgumentException>(() => _detector.GetInfo(null));
        }
    }
}
