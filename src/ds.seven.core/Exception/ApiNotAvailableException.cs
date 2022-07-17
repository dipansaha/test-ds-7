namespace ds.seven.core.Exception;

public class ApiNotAvailableException : System.Exception
{
    public ApiNotAvailableException() : base("External API is not available")
    {
    }
}