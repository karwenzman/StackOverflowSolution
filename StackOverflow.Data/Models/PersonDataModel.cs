namespace StackOverflow.Data.Models;

public class PersonDataModel : ModelBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}
