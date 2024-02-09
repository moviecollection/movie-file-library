using System.Threading.Tasks;
using PublicApiGenerator;
using VerifyXunit;
using Xunit;

namespace MovieFileLibrary.Tests
{
    public class PublicApiTests
    {
        [Fact]
        public Task PublicApiShouldNotChange()
        {
            var publicApi = typeof(MovieDetector).Assembly
                .GeneratePublicApi();

            return Verifier.Verify(publicApi);
        }
    }
}
