using ds.seven.core.Data;
using D = ds.seven.core.Domain;

namespace ds.seven.core.Service;

public class UserService : IUserService
{
    private readonly IDataLoader _dataLoader;

    public UserService(IDataLoader dataLoader)
    {
        _dataLoader = dataLoader;
    }


    public async Task<string?> GetFullNameById(int id)
    {
        var data = await _dataLoader.Load();

        if (data != null && data.All(i => i.Id != id))
        {
            return $"User does not exist with id: {id}";
        }

        return data?.FirstOrDefault(i => i.Id == id)?.ToString();
    }

    public async Task<string?> GetUsersByAge(int age)
    {
        var data = await _dataLoader.Load();

        return data?.Where(u => u.Age == age)
            .Select(i => i.First)
            .Aggregate((x, y) => x + ", " + y);
    }

    public async Task<IEnumerable<D.UserStats>?> FindGenderByAge()
    {
        var data = await _dataLoader.Load();

        return data?.GroupBy(i => i.Age).OrderBy(i=>i.Key)
            .Select(i=> new D.UserStats
            {
                Age = i.Key, 
                Female = i.Count(g=>g.Gender=="F"),
                Male = i.Count(g=>g.Gender=="M")
            }).ToList();
    }
}