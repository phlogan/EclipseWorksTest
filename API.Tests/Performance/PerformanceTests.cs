using EclipseWorksTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Performance
{
    public class PerformanceTests
    {

        [Fact]
        public async Task GetProjectsCreationProcessTime_ReturnsUnauthorized_WhenUserIdIsNotManager()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpClientMock.Object));

            var controller = new PerformanceController(httpClientFactoryMock.Object);

            var result = await controller.GetProjectsCreationProcessTime(2);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("O usuário não tem a função necessária", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetCreateProjectsCount_ReturnsUnauthorized_WhenUserIdIsNotManager()
        {
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientFactoryMock
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(httpClientMock.Object));

            var controller = new PerformanceController(httpClientFactoryMock.Object);

            var result = await controller.GetCreateProjectsCount(2);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("O usuário não tem a função necessária", unauthorizedResult.Value);
        }

    }
}
