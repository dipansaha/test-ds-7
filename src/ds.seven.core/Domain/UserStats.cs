namespace ds.seven.core.Domain;

public class UserStats
{
    public UserStats()
    {
    }

    public UserStats(int age, int female, int male)
    {
        Age = age;
        Female = female;
        Male = male;
    }

    public int Age { get; init; }
    public int Female { get; init; }
    public int Male { get; init; }

    public override string ToString()
    {
        return $"Age: {Age} Female: {Female} Male: {Male}";
    }
} 