using SampleNotifyPropertyChanged.ViewModels.Interfaces;

namespace SampleNotifyPropertyChanged.ViewModels;

public class PersonViewModel : IPersonViewModel
{
    public string FirstName { get; set; } = "Jane";
    public string LastName { get; set; } = "Doe";

    public PersonViewModel()
    {

    }
}
