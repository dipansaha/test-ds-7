using D = ds.seven.core.Domain;

namespace ds.seven.core.Data.Provider;

public interface IDataProvider
{
    Task<IReadOnlyCollection<D.User>?> Load();
}