using D = ds.seven.core.Domain;

namespace ds.seven.core.Service;

public interface IUserService
{
    Task<string?> GetFullNameById(int id);
    Task<string?> GetUsersByAge(int age);
    Task<IEnumerable<D.UserStats>?> FindGenderByAge();
}