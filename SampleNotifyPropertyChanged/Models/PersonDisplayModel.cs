using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Input;

namespace SampleNotifyPropertyChanged.Models;

public partial class PersonDisplayModel : ModelBase
{
    public string FullName => $"{LastName}, {FirstName}";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    private string _firstName = "Jane";

    partial void OnFirstNameChanged(string value)
    {
        Debug.WriteLine($"Event {nameof(OnFirstNameChanged)} was fired.");
        // TODO - how to implement this? is this valid for .net core?
        
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = "Doe";

    partial void OnLastNameChanged(string value)
    {
        Debug.WriteLine($"Event {nameof(OnLastNameChanged)} was fired.");
    }


    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [Range(1, 999, ErrorMessage = "The age must be in the range of 1 to 999")]
    private int _age = 20;

    partial void OnAgeChanged(int value)
    {
        Debug.WriteLine($"Event {nameof(OnAgeChanged)} was fired.");
    }
}
