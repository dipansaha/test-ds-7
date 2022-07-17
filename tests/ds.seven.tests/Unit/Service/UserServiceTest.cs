using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Xunit;

using ds.seven.core.Data;
using ds.seven.core.Domain;
using ds.seven.core.Service;
using ds.seven.tests.Utility;

namespace ds.seven.tests.Unit.Service;

public class UserServiceTest : BaseTest
{
    private readonly IUserService _userService;

    public UserServiceTest()
    {
        var dataLoader = Substitute.For<IDataLoader>();
        dataLoader.Load().Returns((ICollection<User>?) TestData.GetData());
        _userService = new UserService(dataLoader);
    }

    [Fact]
    public async Task Test_GetFullNameById_Should_Return_user_fullname_with_valid_id()
    {
        var response = await _userService.GetFullNameById(41);

        response.Should().Be("Frank Zappa");
    }
    
    [Fact]
    public async Task Test_GetFullNameById_Should_Return_no_user_with_invalid_id()
    {
        var response = await _userService.GetFullNameById(42);

        response.Should().Be("User does not exist with id: 42");
    }
    
    [Fact]
    public async Task Test_GetUsersByAge_Should_Return_users_firstnames_By_Age()
    {
        var response = await _userService.GetUsersByAge(23);

        response.Should().Be("Bill, Frank");
    }
    
    [Fact]
    public async Task Test_FindGenderByAge_Should_Return_gender_count_by_age()
    {
        var expected = new List<UserStats>
        {
            new(age: 23, female: 0, male: 2),
            new(age: 54, female: 0, male: 1),
            new(age: 66, female: 2, male: 1)
        };
        
        var response = await _userService.FindGenderByAge();

        response.Should().BeEquivalentTo(expected);
    }
}