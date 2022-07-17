using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using ds.seven.core.Data.Provider;
using ds.seven.core.Domain;
using ds.seven.tests.Utility;

namespace ds.seven.tests.Unit.Data.Provider;

public class JsonFileDataProviderTest : BaseTest
{
    [Fact]
    public async Task Test_Load_Should_Load_Data_From_File_Source()
    {
        IDataProvider provider = new JsonFileDataProvider();

        var response = await provider.Load();

        response.Should().HaveCount(6).And.ContainItemsAssignableTo<User>();
        response.Should().BeEquivalentTo(TestData.GetData());
    }
}