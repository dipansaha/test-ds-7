using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ds.seven.core.Data.Provider;
using ds.seven.core.DI;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace ds.seven.tests.Unit.Data.Provider;

public class JsonApiDataProviderTest : BaseTest
{
    public JsonApiDataProviderTest() : base(false)
    {
    }
    
    [Fact]
    public async Task Test_Load_Should_Retry_When_Gateway_Timeout()
    {
        var handler = GetHttpMessageHandler(HttpStatusCode.GatewayTimeout);
        Init(handler.Object);

        var provider = DependencyResolver.GetService<IDataProvider>();

        var action = async () =>
        {
            var unused = await provider.Load();
        };

        await action.Should().ThrowAsync<Exception>();
       
        handler.Protected().Verify(
            "SendAsync",
            Times.Exactly(4),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>());
    }
    
    private Mock<HttpMessageHandler> GetHttpMessageHandler(HttpStatusCode statusCode)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(string.Empty)
            })
            .Verifiable();

        return handlerMock;
    }
}
