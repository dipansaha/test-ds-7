namespace ds.seven.core.Domain;

public class User
{
    public int Id { get; set; }
    public string? First { get; set; }
    public string? Last { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }

    public override string ToString()
    {
        return $"{First} {Last}";
    }
}