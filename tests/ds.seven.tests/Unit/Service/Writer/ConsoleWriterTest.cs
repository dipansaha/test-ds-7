using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

using ds.seven.core.Domain;
using ds.seven.core.Service;
using ds.seven.core.Service.Writer;

namespace ds.seven.tests.Unit.Service.Writer;

public class ConsoleWriterTest : BaseTest
{
    [Fact]
    public async Task Test_Write_Should_Return_string()
    {
        var expectedStr = @"#1:The users full name for id=42
Result: User does not exist with id: 42
**************************
#2:All the users first names (comma separated) who are 23
Result: Bill, Frank
**************************
#3:The number of genders per Age, displayed from youngest to oldest
Age: 23 Female: 0 Male: 2
Age: 54 Female: 0 Male: 1
Age: 66 Female: 2 Male: 1
**************************
";
        
        var userService = Substitute.For<IUserService>();
        userService.GetFullNameById(Arg.Is(42)).Returns("User does not exist with id: 42");
        userService.GetUsersByAge(Arg.Is(23)).Returns("Bill, Frank");
        userService.FindGenderByAge().Returns(new List<UserStats>
        {
            new(age: 23, female: 0, male: 2), 
            new(age: 54, female: 0, male: 1), 
            new(age: 66, female: 2, male: 1)
        });

        var writer = new ConsoleWriter(userService);
        var response = await writer.Write();

        response.Should().Be(expectedStr);
        await userService.Received(1).GetFullNameById(Arg.Is(42));
        await userService.Received(1).GetUsersByAge(Arg.Is(23));
        await userService.Received(1).FindGenderByAge();
    }
}