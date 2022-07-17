namespace ds.seven.core.Service.Writer;

public interface IWriter<T>
{
    Task<T> Write();
}