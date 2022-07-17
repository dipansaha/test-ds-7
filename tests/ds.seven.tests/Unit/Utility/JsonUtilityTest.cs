using ds.seven.core.Utility;
using FluentAssertions;
using Xunit;

namespace ds.seven.tests.Unit.Utility;

public class JsonUtilityTest
{
    [Fact]
    public void Test_ToJsonString_Should_Return_string()
    {
        var testObj = new {Firstname = "John", Lastname = "Smith"};

        testObj.ToJsonString().Should().Be("{\"firstname\":\"John\",\"lastname\":\"Smith\"}");
    }
    
    [Fact]
    public void Test_FromJsonString_Should_Return_Object()
    {
        const string testJsonString = "{\"firstname\":\"John\",\"lastname\":\"Smith\"}";
        var expected = new TestClass {Firstname = "John", Lastname = "Smith"};

        testJsonString.FromJsonString<TestClass>().Should().BeEquivalentTo(expected);
    }

    private class TestClass
    {
        public string? Firstname { get; init; }
        public string? Lastname { get; init; } 
    }
}