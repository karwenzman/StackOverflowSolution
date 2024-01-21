using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleNotifyPropertyChanged.Models;

public partial class PersonDisplayModel : ModelBase
{
    public string FullName => $"{LastName}, {FirstName}";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    private string _firstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [Range(1, 999, ErrorMessage = "The age must be in the range of 1 to 999")]
    private int _age;
}
