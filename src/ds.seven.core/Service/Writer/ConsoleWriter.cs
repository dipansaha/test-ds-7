using System.Text;

namespace ds.seven.core.Service.Writer;

public class ConsoleWriter : IWriter<string>
{
    private readonly IUserService _userService;

    public ConsoleWriter(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<string> Write()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("#1:The users full name for id=42");
        var result1 = await _userService.GetFullNameById(42);
        sb.AppendLine($"Result: {result1}");
        
        sb.AppendLine("**************************");
        
        sb.AppendLine("#2:All the users first names (comma separated) who are 23");
        var result2 = await _userService.GetUsersByAge(23);
        sb.AppendLine($"Result: {result2}");
        
        sb.AppendLine("**************************");
        
        sb.AppendLine("#3:The number of genders per Age, displayed from youngest to oldest");
        var result3 = await _userService.FindGenderByAge();
        if (result3 != null)
            foreach (var userStats in result3)
            {
                sb.AppendLine($"{userStats}");
            }
        
        sb.AppendLine("**************************");

        return sb.ToString();
    }
}