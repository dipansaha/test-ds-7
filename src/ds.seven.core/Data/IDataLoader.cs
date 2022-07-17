using D = ds.seven.core.Domain;

namespace ds.seven.core.Data;

public interface IDataLoader
{
    Task<ICollection<D.User>?> Load();
}