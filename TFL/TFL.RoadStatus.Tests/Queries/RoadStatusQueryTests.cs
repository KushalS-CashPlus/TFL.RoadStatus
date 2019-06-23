using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using TFL.RoadStatus.Factories;
using TFL.RoadStatus.Queries;
using Xunit;

namespace TFL.RoadStatus.Tests.Queries
{
    public class RoadStatusQueryTests
    {
        readonly RoadStatusQuery sut;
        readonly Mock<ITflHttpClientFactory> tflHttpClientFactory = new Mock<ITflHttpClientFactory>();

        public RoadStatusQueryTests()
        {
            sut = new RoadStatusQuery(tflHttpClientFactory.Object);
        }

        [Fact]
        public async Task SHOULD_throw_exception_WHEN_invalid_road_is_provided()
        {
            // Arrange
            tflHttpClientFactory.Setup(x => x.Create())
                                .Returns(new HttpClient(new FakeNotFoundHttpHandler()) { BaseAddress = new Uri("http://localhost") })
                                .Verifiable();

            // Act and Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await sut.Get(It.IsAny<string>()));
            tflHttpClientFactory.VerifyAll();
        }

        [Theory]
        [InlineData("A2", 1)]
        [InlineData("A2,A406", 2)]
        public async Task SHOULD_return_data_WHEN_valid_road_is_provided(string road, int resultCount)
        {
            // Arrange
            var responseContent = GetRoadStatusTestData(road);
            tflHttpClientFactory.Setup(x => x.Create())
                                .Returns(new HttpClient(new FakeOkHttpHandler(responseContent)) { BaseAddress = new Uri("http://localhost") })
                                .Verifiable();

            // Act
            var actualResult = await sut.Get(road);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(resultCount, actualResult.Count);
            tflHttpClientFactory.Verify(x => x.Create(), Times.Once);
        }

        private string GetRoadStatusTestData(string fileName)
        {
            var json = File.ReadAllText($"TestData/{fileName}.json");
            return json;
        }
    }
}
