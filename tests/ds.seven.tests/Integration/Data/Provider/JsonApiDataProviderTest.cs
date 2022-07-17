using System.Threading.Tasks;
using ds.seven.core.Data.Provider;
using ds.seven.core.DI;
using FluentAssertions;
using Xunit;

namespace ds.seven.tests.Integration.Data.Provider;

public class JsonApiDataProviderTest : BaseTest
{
    [Fact]
    public async Task Test_Load_Should_Return_Users()
    {
        var provider = DependencyResolver.GetService<IDataProvider>();

        var response = await provider.Load();

        response.Should().HaveCountGreaterThan(0);
    }
}