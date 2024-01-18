namespace SampleNotifyPropertyChanged.Models;

public class PersonDisplayModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{LastName}, {FirstName}";
    public int Age { get; set; }
}
